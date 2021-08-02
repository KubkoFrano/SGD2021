// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "m1r0/SphereInCube"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _Color("Material Color", Color) = (.25, .5, .5, 1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        //LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct VertexOutput
            {
                float3 wPos : TEXCOORD0;
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD3;
                float3 normal : TEXCOORD1;
                float3 clipSpace : TEXCOORD2;
                float3 vertex : TEXCOORD4;
            };

            float4 _Color;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.uv;

                o.normal = UnityObjectToWorldNormal( v.normal );
                o.clipSpace = UnityObjectToClipPos(v.vertex);
                o.vertex = v.vertex;
                return o;
            }

            #define STEPS 64
            #define STEP_SIZE 0.01
            #define TAU 6.2831853071
            
            bool SphereHit(float3 p, float3 S, float r, float2 scale) {
                //p = S + (S-p) * float3(scale.x, scale.y, scale.x);
                return distance(p, S) < r;
            }
            
            float RaymarchHit(float3 position, float3 direction, float3 S, float r, float2 scale)
            {
                
                for (int i = 0; i < STEPS; i++) {
                    if (SphereHit(position, S, r, scale))
                        return 0;
                    position += direction * STEP_SIZE;
                }

                return 1;
            }

            float rnd(float2 uv) {
                return frac(sin(dot(uv, float2(7.2345689, 21.8764))) * 45.545374);
            }

            float simpleNoise(float2 st) {
                float2 ipos = floor(st);
                float2 fpos = frac(st);

                float a = rnd(ipos);
                float b = rnd(ipos + float2(1, 0));
                float c = rnd(ipos + float2(0, 1));
                float d = rnd(ipos + float2(1, 1));

                float2 u = fpos * fpos * (3. - 2. * fpos);

                return lerp(a, b, u.x) +
                    (c - a) * u.y * (1. - u.x) +
                    (d - b) * u.x * (u.y);
            }

            float circle(float2 uv, float2 scale) {
                uv -= .5;
                uv *= scale;
                return step(dot(uv, uv), .25);
            }

            float posterize(float input)
            {
                return ceil(max(1, input) * 6) * .1;
            }


            fixed4 frag(VertexOutput i) : SV_Target
            {

                float3 objectPosition = mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz;
                float3 viewDir = normalize(i.wPos - _WorldSpaceCameraPos);
                float3 worldPos = i.wPos;

                float2 uv = float2( sin(i.uv.x*TAU) * .5 + .5 , frac(i.vertex.y + .5));
                float2 noiseUV = (uv + _Time.y * float2(0, -.5)) * 10.;

                float f = simpleNoise(noiseUV);
                f = lerp(f, simpleNoise(noiseUV * 2.), .33);
                f = lerp(f, simpleNoise(noiseUV * 4.), .1);

                f = f - .8;

                f *= pow(uv.y, 1.3);

                float2 distortion = uv + float2(0, f);

                float p = 1 - distortion.y;

                p = pow(p, 2.7);
                //float x = 1 - RaymarchHit(worldPos, viewDir, objectPosition, .4);
                worldPos = float3(worldPos.x, worldPos.y + p, worldPos.z);
                float s = 1 - RaymarchHit(worldPos, viewDir, objectPosition, .3, float2(4, 3));
                float c = 1 - RaymarchHit(worldPos, viewDir, objectPosition, .4, float2(3, 2));    //float2(2.5, 1.66)
                float a = 1 - RaymarchHit(worldPos, viewDir, objectPosition, .5, float2(2.1, 1.3));


                p += .4;
                p *= 1.5;
                p = posterize(p);

                
                float final = p + c + s;

                float3 color = final * _Color;
                //return float4((x.xxx), 1.);
                //return float4((objectPosition.y - .5).xxx, 1);

                return float4(color, a);
                //else            return float4(1, 1, 1, 0);

            }
            ENDCG
        }
    }
}
