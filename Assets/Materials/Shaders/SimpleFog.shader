Shader "m1r0/SimpleFog"
{
    Properties
    {
       _Color("Main Color", Color) = (1, 1, 1, .5)
       _Scale("Scale", float) = 1.
       _IntersectionThresholdMax("Intersection Threshold Max", float) = 1
       
    }
        SubShader
    {
        Tags { 
               "RenderType" = "Transparent" 
               "Queue" = "Transparent"
             }

        Pass
        {
           Blend SrcAlpha OneMinusSrcAlpha
           ZWrite Off

           CGPROGRAM
           #pragma vertex vert
           #pragma fragment frag
           #pragma multi_compile_fog
           #include "UnityCG.cginc"

           struct appdata
           {
               float4 vertex : POSITION;
               float2 uv : TEXCOORD0;
           };

           struct v2f
           {
               float4 scrPos : TEXCOORD0;
               UNITY_FOG_COORDS(1)
               float4 vertex : SV_POSITION;
               float2 uv : TEXCOORD1;
           };

           sampler2D _CameraDepthTexture;
           float4 _Color;
           float4 _IntersectionColor;
           float _IntersectionThresholdMax;
           float _Scale;

           v2f vert(appdata v)
           {
               v2f o;
               o.vertex = UnityObjectToClipPos(v.vertex);
               o.scrPos = ComputeScreenPos(o.vertex);
               UNITY_TRANSFER_FOG(o,o.vertex);
               o.uv = v.uv;
               return o;
           }

           float rnd(float2 uv) {
               return frac(sin(dot(uv, float2(7.4657862, 13.162782512))) * 123.455772);
           }

           float ValueNoise(float2 uv) {
               float2 f = frac(uv);
               float2 i = floor(uv);

               float a = rnd(i + float2(0., 0.));
               float b = rnd(i + float2(1., 0.));
               float c = rnd(i + float2(0., 1.));
               float d = rnd(i + float2(1., 1.));

               float2 u = f * f * (3 - 2 * f);

               return lerp(a, b, u.x) +
                          (c - a) * u.y * (1. - u.x) +
                          (d - b) * u.x * u.y;
           }

            half4 frag(v2f i) : SV_TARGET
            {
               float depth = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)));
               float diff = saturate(_IntersectionThresholdMax * (depth - i.scrPos.w));

               float f = ValueNoise(i.uv * _Scale);
               f = lerp(f, ValueNoise(i.uv * _Scale * 2.), .5);
               f = lerp(f, ValueNoise(i.uv * _Scale * 4.), .3);
               f += 0.2;
               float4 color = float4(_Color.xyz, f);

               fixed4 col = lerp(fixed4(_Color.rgb, 0.0), color, diff * diff * diff * (diff * (6 * diff - 15) + 10));

               UNITY_APPLY_FOG(i.fogCoord, col);
               return col;
            }

            ENDCG
        }
    }
}
