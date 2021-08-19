Shader "m1r0/Luminate"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _Color1("Color1", Color) = (1., 1., 1., 1.)
        _Color2("Color2", Color) = (1., 1., 1., 1.)
        _Strenght("Strenght", float) = 1.
    }
    SubShader
    {
        Tags { 
                "RenderType"="Transparent"
                "Queue"="Transparent"
             }
        LOD 100
        
        Cull Off
        ZWrite Off
        //Blend SrcAlpha OneMinusSrcAlpha
        Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color1;
            float4 _Color2;
            float _Strenght;

            v2f vert (appdata v) 
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2x2 rotate(float angle)
            {
                return float2x2(cos(angle), -sin(angle),
                                sin(angle), cos(angle));
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = .5 - i.uv;
                uv = mul(rotate(_Time.y / 2.), uv);

                float w = max(0, .15 - dot(uv, uv));

                float c = length(uv);
                float a = asin(uv.y / c);
                
                a = sin(a * 7.);
                
                a = max(0, a);
                float d = max(0., .25 - dot(uv, uv));

                
                a *= d;
                //float3 color = _Color1 * step(0, a) + _Color2 * step(0, w);
                //return float4(color * (a + w), 1.);
                return float4(_Color2.rgb * (w * _Strenght), 0);
            }
            ENDCG
        }
    }
}
