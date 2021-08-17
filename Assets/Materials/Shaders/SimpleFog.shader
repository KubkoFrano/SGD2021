Shader "m1r0/SimpleFog"
{
    Properties
    {
       _Color("Main Color", Color) = (1, 1, 1, .5)
       _Scale("Noise Scale", float) = 1.
       _NoiseStrenght("Noise Strenght", float) = .05
       _IntersectionThresholdMax("Intersection Threshold Max", float) = 1
       _MainTex("Texture", 2D) = "black" {}
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent"  }

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
           sampler2D _MainTex;
           float4 _Color;
           float4 _IntersectionColor;
           float _IntersectionThresholdMax;
           float _Scale;
           float _NoiseStrenght;


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
               return frac(sin(dot(uv, float2(12.3456789, 5.64738291))) * 12.456678197);
           }

           float valueNoise(float2 uv) {
               float2 f = frac(uv);
               float2 i = floor(uv);

               float a = rnd(i + float2(0., 0.));
               float b = rnd(i + float2(1., 0.));
               float c = rnd(i + float2(0., 1.));
               float d = rnd(i + float2(1., 1.));
               
               float2 u = f * f * (3. - 2. * f);

               return lerp(a, b, u.x) +
                          (c - a) * u.y * (1. - u.x) +
                          (d - b) * u.x * u.y;
           }

           half4 frag(v2f i) : SV_TARGET
           {
               float depth = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)));
               float diff = saturate(_IntersectionThresholdMax * (depth - i.scrPos.w));

               float2 uv = i.uv * _Scale;

               float f = valueNoise(uv + _Time.y * float2(1., 0.));
               f = lerp(f, valueNoise(uv * 2. + _Time.y * float2(.5, -.7)), .5);
               f = lerp(f, valueNoise(uv * 4. + _Time.y * float2(-1., .3)), .3);

               //float4 color = float4(_Color.xyz, f);
               float4 color = float4(_Color.xyz + f * _NoiseStrenght, _Color.w);

               fixed4 col = lerp(fixed4(color.rgb, 0.0), color, diff * diff * diff * (diff * (6 * diff - 15) + 10));

               UNITY_APPLY_FOG(i.fogCoord, col);


               //return float4(depth.xxx * _IntersectionThresholdMax, 1.);
               return col;
            }

            ENDCG
        }
    }
}
