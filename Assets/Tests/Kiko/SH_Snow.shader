Shader "URP/SnowShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _SnowAmount("Snow amount", Range(0,1)) = 0.5
        _SnowAngle("Snow angle", Vector) = (0,-1,0,1)
        _SnowColor("Snow color", Color) = (1,1,1,1)
        _Height("Snow height", float) = 0.1
        _RimPower("Rim light", float) = 2.0
        [HDR] _RimColor("Rim color", Color) = (1,1,1,1)
    }

        SubShader
        {
            Tags { "RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Opaque" }
            Pass
            {
                Name "MainPass"
                Tags { "LightMode" = "UniversalForward" }

                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Define las propiedades
            sampler2D _MainTex;
            float4 _Color;
            float _Glossiness;
            float _Metallic;
            float _SnowAmount;
            float4 _SnowAngle;
            float4 _SnowColor;
            float _Height;
            float _RimPower;
            float4 _RimColor;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION; // Clip-space position
                float3 worldPos : TEXCOORD1;     // World-space position
                float3 normalWS : TEXCOORD2;    // World-space normal
                float2 uv : TEXCOORD0;          // UV coordinates
                float3 viewDirWS : TEXCOORD3;   // View direction in world space
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                // Transform position to world space
                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);

                // Convert SnowAngle to world space
                float3 snowC = mul((float3x3)unity_ObjectToWorld, _SnowAngle.xyz);

                // Adjust vertex position based on snow logic
                if (dot(IN.normalOS, snowC) >= 1.0 - _SnowAmount)
                {
                    worldPos += normalize(mul((float3x3)unity_ObjectToWorld, IN.normalOS)) * _Height;
                }

                // Transform position to clip space
                OUT.positionHCS = TransformWorldToHClip(worldPos);

                // Pass additional data to the fragment shader
                OUT.worldPos = worldPos;
                OUT.normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));
                OUT.uv = IN.uv;
                OUT.viewDirWS = normalize(GetCameraPositionWS() - worldPos);

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Sample the texture
                float4 baseColor = tex2D(_MainTex, IN.uv) * _Color;

                // Calculate snow coverage
                float3 snowDir = normalize(_SnowAngle.xyz);
                float snowCoverage = dot(IN.normalWS, snowDir);

                // Initialize output color
                float4 finalColor;

                if (snowCoverage >= 1.0 - _SnowAmount)
                {
                    // Apply snow color
                    finalColor.rgb = _SnowColor.rgb;

                    // Add rim lighting
                    float rim = 1.0 - saturate(dot(normalize(IN.viewDirWS), IN.normalWS));
                    finalColor.rgb += _RimColor.rgb * pow(rim, _RimPower);
                }
                else
                {
                    // Apply base material color
                    finalColor.rgb = baseColor.rgb;
                }

                // Set metallic and smoothness
                finalColor.a = baseColor.a; // Alpha
                return finalColor;
            }

            ENDHLSL
        }
        }

            FallBack "Diffuse"
}
