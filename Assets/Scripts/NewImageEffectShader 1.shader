// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/NewImageEffectShader 1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

       Pass
        {
          CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag
          #include "UnityCG.cginc"

          ///////////////////////////////////////////////////////////////////

          struct VS_INPUT
          {
             float4 vertex  : POSITION;
             float3 normal  : NORMAL;
             float3 tangent  : TANGENT0;
             float2 texCoord  : TEXCOORD0;
          };

          struct VS_OUTPUT
          {
             float4 pos      : POSITION;
             float3 normal   : TEXCOORD0;
             float3 tangent  : TEXCOORD1;
             float3 viewVec  : TEXCOORD2;
             float2 texCoord : TEXCOORD3;
             float  ambOcc : TEXCOORD4;
             float3 lightVec : TEXCOORD5;
          };

          VS_OUTPUT vert(VS_INPUT i)
          {
            VS_OUTPUT o = (VS_OUTPUT)0;

            // Output transformed position:
            o.pos = UnityObjectToClipPos(i.vertex);

            // Output light vector:
            o.lightVec = normalize(ObjSpaceLightDir(i.vertex));

            // Compute position in view space:
            float3 Pview = mul(UNITY_MATRIX_MV, i.vertex); //float3 Pview = mul(matView, i.vertex); 

            // hair model doesn't contain per-vertex ambient occlusion
            o.ambOcc = 1.0f;

            // Transform the input normal to view space:
            o.normal = i.normal;//normalize( mul( matView, tempNorm ) );   

            o.tangent = i.tangent;

            // Compute the view direction in view space:
            o.viewVec = -normalize(Pview);

            // Propagate texture coordinate for the object:
            o.texCoord = i.texCoord;

            return o;
            }

          ///////////////////////////////////////////////////////////////////

          struct PS_INPUT
          {
             float2 texCoord : TEXCOORD3;
          };

          half4 frag(PS_INPUT i) : COLOR
          {
            float4 o;

            o.rgb = _WorldSpaceLightPos0.xyz;
            o.a = 1;

            return o;
          }

      ENDCG
    }

        }
    }

