Shader "Custom/JB_MultiColor" 
{
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos	: SV_POSITION;
				float4 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			fixed4 _Color;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{ 
				fixed4 c = tex2D(_MainTex, i.uv);
				return c * _Color;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
