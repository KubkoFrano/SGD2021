// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "m1r0/LuminateCoin"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _Color1("Main Color", Color) = (1., 1., 1., 1.)
        _Color2("Border Color", Color) = (1., 1., 1., 1.)
        _Strenght("Strenght", float) = 1.
        _Difference("Difference", float) = 2.
        _Color3("Gloss Color", Color) = (1., 1., 1., 1.)
        _Gloss("Gloss", float) = 1.
        _Density("Density", float) = 1.
    }
    SubShader
    {
        Tags { 
                "RenderType"="Transparent"
                "Queue"="Transparent"
             }
        LOD 100
        
        //Cull Off
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float _Strenght;
            float _Difference;
            float _Gloss;
            float _Density;

            v2f vert (appdata v) 
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            /*
            float RND(float2 uv) {
                return frac(sin(dot(uv, float2(13.6298614, 7.1398561))) * 123.456789);
            }

            float ValueNoise(float2 uv) {
                float2 f = frac(uv);
                float2 i = floor(uv);

                float a = RND(i + float2(0., 0.));
                float b = RND(i + float2(1., 0.));
                float c = RND(i + float2(0., 1.));
                float d = RND(i + float2(1., 1.));

                float2 u = f * f * (3. - 2. * f);

                return lerp(a, b, u.x) +
                           (c - a) * u.y * (1. - u.x) +
                           (d - b) * u.x * u.y;
            }

            float BM(float2 uv) {
                return ValueNoise(uv * 4.) + ValueNoise(uv * 8.) + ValueNoise(uv * 16.);
            }
            */

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                
                float3 camDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 normals = normalize(i.normal);
                float3 LightDir = normalize(_WorldSpaceLightPos0);

                float f = dot(camDir, normals);
                //float reflection = max(0., dot(reflect(-camDir, normals), LightDir));

                //reflection = pow(reflection, _Gloss);

                //float3 reflectColor = reflection * _Color3;
                //return reflection.xxxx;
                f = pow(f , _Strenght);
                

                //f = 1. - f;
                float3 color = lerp(_Color1.rgb, _Color2.rgb, pow(f, _Difference)) * f;
                //color = lerp(color, reflectColor,  reflection);
                return float4(saturate(color) * _Density, 1.);
            }
            ENDCG
        }
    }
}
