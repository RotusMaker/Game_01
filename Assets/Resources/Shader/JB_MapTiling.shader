Shader "Custom/JB_MapTiling" 
{
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Tiling ("Tiling Vector", Vector) = (0,0,0,0)
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
			float4 _Tiling;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = float4(v.texcoord.x * _Tiling.x, v.texcoord.y * _Tiling.y, 0, 0);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{ 
				fixed4 c = tex2D(_MainTex, i.uv);
				return c;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
