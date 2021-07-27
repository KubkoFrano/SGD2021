Shader "Unlit/unlitShader"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _Color("Material Color", Color) = (.25, .5, .5, 1)
        _BackLight("BackLight", Color) = (.15, .1, .075, 1)
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
            float4 _BackLight;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal( v.normal );
                o.clipSpace = UnityObjectToClipPos(v.vertex);
               
                return o;
            }

            fixed4 frag(VertexOutput i) : SV_Target
            {
                
                float3 lightDir = _WorldSpaceLightPos0;

                float f = max(0, dot(lightDir, i.normal));
                float3 color = _Color * f;
                
                color += _BackLight;
                //color += half3(unity_SHAr.w, unity_SHAg.w, unity_SHAb.w);

                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}
