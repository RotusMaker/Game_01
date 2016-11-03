// Toony Colors Pro+Mobile Shaders
// (c) 2013, Jean Moreno

Shader "Toony Colors Pro/Normal/OneDirLight/Bumped Specular Shadow"
{
	Properties
	{
		_MainTex ("Base (RGB) Gloss (A) ", 2D) = "white" {}
		_BumpMap ("Normal map (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		_Color ("Highlight Color", Color) = (0.8,0.8,0.8,1)
		_SColor ("Shadow Color", Color) = (0.0,0.0,0.0,1)
	}

	SubShader
	{
		Tags { "Queue" = "Geometry+1500" "RenderType"="Opaque" }
		LOD 200

		// joonbumpark 20161102
		// 주사위 그림자 쉐이더 분석 및 설명 추가.

		// Pass for Plannar Shadow
		Pass
		{
			Tags { "RenderType"= "Transparent"
				"LightMode" = "ForwardBase" }
			// rendering of projected shadow
			Offset -1.0, -2.0
			// make sure shadow polygons are on top of shadow receiver

			ZWrite Off

			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
			Stencil {
				Ref 0
				Comp Equal
				Pass IncrSat
				ZFail decrSat
			}

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma debug

			#include "UnityCG.cginc"

			// User-specified uniforms
			uniform float4 _ShadowColor;
			uniform float4 _Color;
			uniform float4x4 _World2Receiver; // transformation from world coordinates to the coordinate system of the plane

			float4 vert( float4 vertexPos : POSITION ) : SV_POSITION
			{
				float4x4 world2Object = unity_WorldToObject * 1.0;
				world2Object[3][3] = 1.0;	//transform.z = 1.0

				// UNITY_MATRIX_MV: Current model * view matrix.
				float4x4 viewMatrix = mul(UNITY_MATRIX_MV, world2Object);

				// < ilmeda, 20161012
				//float4 lightDirection;
				//if (0.0 != _WorldSpaceLightPos0.w)
				//{
				//	// point or spot light
				//	lightDirection = normalize( mul(modelMatrix, vertexPos - _WorldSpaceLightPos0));
				//}
				//else
				//{
				//	// directional light
				//	lightDirection = -normalize(_WorldSpaceLightPos0);
				//}

				// Light’s position in object space. w component is 0 for directional lights, 1 for other lights
				// "L" Direction Light Normal Vector
				float4 lightDirection = -normalize(_WorldSpaceLightPos0);
				// ilmeda, 20161012 >

				// "P" Vector
				float4 vertexInWorldSpace = mul(unity_ObjectToWorld, vertexPos);
				// 평면의 Up Vector
				float4 worldPlaneUpVec = _World2Receiver._m01_m11_m21_m31;
				// "H" = "P"벡터의 평면으로 부터의 높이.
				float distanceOfVertex = dot(worldPlaneUpVec, vertexInWorldSpace - _World2Receiver._m03_m13_m23_m33);
				// "Y" = "L"을 빗변으로 하는 삼각형의 높이.
				float lengthOfLightDirectionInY = dot(worldPlaneUpVec, lightDirection);
				// "P'" = "P"에서 "L"방향으로 진행 시 평면과 교차점.
				if (distanceOfVertex > 0.0 && lengthOfLightDirectionInY < 0.0)
				{
					// P->P':L(=1) = H:Y
					// H = P->P'*Y
					// P->P' = H/Y
					// "Y" 를 구할 때 실제 y-axis와 반대로 구했기 때문에 minus 기호: P->P' = H/(-1)Y
					// 광원 벡터 = L(방향) * P->P'(크기)
					lightDirection = lightDirection * (distanceOfVertex / (-lengthOfLightDirectionInY));
				}
				else
				{
					// don't move vertex
					lightDirection = float4(0.0, 0.0, 0.0, 0.0);
				}
				// "P'"벡터 = "P"벡터 + 광원 벡터 
				// Shadow Vertex Pos = "P'"벡터 * Projection Matrix
				return mul(UNITY_MATRIX_P, mul(viewMatrix, vertexInWorldSpace + lightDirection));
			}

			float4 frag( void ) : COLOR
			{
				float4 returnColor = _ShadowColor;
				returnColor.a *= _Color.a;
				return returnColor;
			}

			ENDCG
		}

		Pass
		{
			ZWrite On
			ColorMask 0
		}

		CGPROGRAM

		#include "TGP_Include.cginc"

		//nolightmap nodirlightmap		LIGHTMAP
		//approxview halfasview		SPECULAR/VIEW DIR
		//noforwardadd				ONLY 1 DIR LIGHT (OTHER LIGHTS AS VERTEX-LIT)
		#pragma surface surf ToonyColorsSpec nolightmap nodirlightmap noforwardadd approxview halfasview

		sampler2D _MainTex;
		sampler2D _BumpMap;
		fixed _Shininess;

		struct Input
		{
			half2 uv_MainTex : TEXCOORD0;
			half2 uv_BumpMap : TEXCOORD1;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);

			o.Albedo = c.rgb;
			o.Alpha = _Color.a;

			//Specular
			o.Gloss = c.a;
			o.Specular = _Shininess;
			//Normal map
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}

	Fallback "Toony Colors Pro/Normal/OneDirLight/Bumped"
}
