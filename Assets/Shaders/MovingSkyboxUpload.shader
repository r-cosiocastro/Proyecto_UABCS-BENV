// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MovingSkyboxUpload" {
      Properties {
          _CubeMap ("Skybox Cube Map", cube) = "grey" {}
      }
      SubShader {
          Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
          Cull Off ZWrite Off
 
          Pass{
          CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag
 
          //        --------------------    [   un-comment For HDRI Cubemaps   ]    --------------------
          //#include "UnityCG.cginc"
          //half4 _CubeMap_HDR;
          //        --------------------    [   un-comment For HDRI Cubemaps   ]    --------------------
 
          uniform samplerCUBE _CubeMap;
       
          struct vertexInput{
              float4 vertex: POSITION;
          };
          struct vertexOutput{
              float4 pos : SV_POSITION;
              float4 posWorld : TEXCOORD1;
              fixed3x3 R : TEXCOORD4;
          };
       
          vertexOutput vert(vertexInput v){
              vertexOutput o;
              o.pos = UnityObjectToClipPos(v.vertex);
              o.posWorld = mul(unity_ObjectToWorld, v.vertex);
   
              fixed3 new_up = normalize(_WorldSpaceLightPos0.xyz);
  //        folowing code from http://stackoverflow.com/questions/32257338/vertex-position-relative-to-normal
              float3 up = float3(0,1,0);
   
              float3 w = cross(up, new_up);
              float s = length(w);  // Sine of the angle
              float c = dot(up, new_up); // Cosine of the angle
              float3x3 VX = float3x3 (
                  0, -1 * w.z, w.y,
                  w.z, 0, -1 * w.x,
                  -1 * w.y, w.x, 0
              ); // This is the skew-symmetric cross-product matrix of v
              float3x3 I = float3x3 (
                  1, 0, 0,
                  0, 1, 0,
                  0, 0, 1
              ); // The identity matrix
           
              o.R = I + VX + mul(VX , VX) * (1 - c)/(s*s); // The rotation matrix! YAY!
  //        previous code from http://stackoverflow.com/questions/32257338/vertex-position-relative-to-normal
              return o;
          }
       
          float4 frag(vertexOutput i) : COLOR{
              float3 viewDirection = normalize( _WorldSpaceCameraPos.xyz - i.posWorld.xyz );
              viewDirection = mul (viewDirection , i.R);
              //        --------------------    [   un-comment For HDRI Cubemaps   ]    --------------------
              //return fixed4(DecodeHDR (texCUBE(_CubeMap, viewDirection), _CubeMap_HDR),1);
              //        --------------------    [   un-comment For HDRI Cubemaps   ]    --------------------
              return texCUBE(_CubeMap, viewDirection);//        if upside down, add - in front of viewDirection
          }
          ENDCG
      }
  }
  }
