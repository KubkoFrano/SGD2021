Shader "m1r0/FlamePowerUp"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _Color("Material Color", Color) = (.25, .5, .5, 1)
        
    }
        SubShader
    {
        Tags { 
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
             }
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 clipSpace : TEXCOORD2;

            };

            float4 _Color;
            
            

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.clipSpace = UnityObjectToClipPos(v.vertex);

                return o;
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
                float2 uv = i.uv;
                float2 noiseUV = (uv + _Time.y * float2(0, -.5)) * 8.;
                
                float f = simpleNoise(noiseUV);
                      f = lerp(f, simpleNoise(noiseUV * 2.), .33);
                      f = lerp(f, simpleNoise(noiseUV * 4.), .1);
                
                f = f - .8;

                f *= pow(uv.y, 1.3);

                float2 distortion = uv + float2(0, f);

                float p = 1 - distortion.y;

                p = pow(p, 2.7);   
                float c = circle(float2(distortion.x, p), float2(2.5, 1.66));
                float a = circle(float2(distortion.x, p), float2(2.1, 1.3));

                p += .4;
                p *= 1.5;
                p = posterize(p);

                float final = p + c;

                float3 color = final * _Color;

                

                return float4(color, a);
                



            }
            ENDCG
        }
    }
}
