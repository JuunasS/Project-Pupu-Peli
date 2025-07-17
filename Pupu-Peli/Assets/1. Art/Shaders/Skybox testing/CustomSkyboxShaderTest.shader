Shader "Unlit/CustomSkyboxShaderTest"
{
    Properties
    {
        
        _MainTex ("Texture", 2D) = "white" {}
        _SampleMap("Sample Map", 2D) = "black"

        _MainTex2 ("Texture2", 2D) = "white" {}

        _TransitionValue("TransitionValue", Range(0, 1)) = 0
        _RotationSpeed("RotationSpeed", Float) = 0.1
        
        _SunRadius ("Sun radius", Range(0,1)) = 0.05
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
        Cull Off 
        ZWrite Off

        Pass
        {
             HLSLPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float3 _SunDir, _MoonDir;
            
            float _SunRadius;
            //sampler2D _MainTex;

            

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_SampleMap);
            SAMPLER(sampler_SampleMap);

            sampler2D _MainTex_ST;
            float4 _SampleMap_ST;

            struct Attributes
            {
                float4 posOS    : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 posCS        : SV_POSITION;
                float3 viewDirWS    : TEXCOORD0;
            };

            v2f Vertex(Attributes IN)
            {
                v2f OUT = (v2f)0;

                VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.posOS.xyz);
                
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.posCS = TransformObjectToHClip(IN.posOS.xyz);
                
                //OUT.posCS = vertexInput.positionCS;
                OUT.viewDirWS = vertexInput.positionWS;
                

                return OUT;
            }

            float GetSunMask(float sunViewDot, float sunRadius)
            {
                float stepRadius = 1 - sunRadius * sunRadius;
                return step(stepRadius, sunViewDot);
            }

            half4 Fragment (v2f IN) : SV_TARGET
            {
                float3 viewDir = normalize(IN.viewDirWS);

                // Main angles
                float sunViewDot = dot(_SunDir, viewDir);
                float sunZenithDot = _SunDir.y;
                float viewZenithDot = viewDir.y;
                float sunMoonDot = dot(_SunDir, _MoonDir);

                float sunViewDot01 = (sunViewDot + 1.0) * 0.5;
                float sunZenithDot01 = (sunZenithDot + 1.0) * 0.5;
                
                // The sun
                float sunMask = GetSunMask(sunViewDot, _SunRadius);
                float3 sunColor = _MainLightColor.rgb * sunMask;

                float3 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.viewDirWS) + sunColor;
                return float4(col, 1);
                
            }

            ENDHLSL
        }
    }
}
