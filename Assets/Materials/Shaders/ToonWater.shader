Shader "m1r0/CartoonWater"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _Color1("Color1", Color) = (.25, .5, .5, 1)
        _Color2("Color2", Color) = (.25, .5, .5, 1)
        _Color3("Color3", Color) = (.25, .5, .5, 1)
        _Scale("Scale", float) = 1.
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        //LOD 100

        Pass
        {
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
            };

            struct VertexOutput
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float _Scale;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
               
                return o;
            }

            float2 rnd2(float2 uv) {
                float2 h = float2(dot(uv, float2(7.13649, 16.3820561)),
                                  dot(uv, float2(13.57294, 5.3729413)));
                return frac(sin(h) * 471936.16391);
            }

            float worley(float2 uv) {

                float2 i = floor(uv);
                float2 f = frac(uv);
                

                float o = 8.;
                for (float x = -1.; x <= 1.; x++)
                    for (float y = -1.; y <= 1.; y++)
                    {
                        float2 iPos = float2(x, y);

                        float2 r = rnd2(i + iPos);

                        r = .5 + .5 * cos(_Time.y + 6.28 * r);

                        float d = pow(length(iPos + r - f), .7);

                        float h = step(0., o - d);
                        o = lerp(o, d, h);
                    }

                return o;
            }
             
            fixed4 frag(VertexOutput i) : SV_Target
            {
                float f = worley(i.uv * _Scale);
                float w = worley(i.uv * _Scale * 3./2.);

                f = pow(f, 5.);
                w = pow(w, 7.);

                float3 color = _Color1 + f * _Color2 + w * _Color3;
                
                return float4(color, 1.);
            }
            ENDCG
        }
    }
}
