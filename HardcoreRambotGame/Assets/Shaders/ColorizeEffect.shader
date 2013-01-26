Shader "Hidden/Colorize Effect" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Color ("Color", COLOR) = (1,0,0,1)
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
				
CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform sampler2D _RampTex;
uniform half _RampOffset;
uniform fixed4 _Color;

fixed4 frag (v2f_img i) : COLOR
{
	fixed4 original = tex2D(_MainTex, i.uv);
	fixed4 output = original * _Color;
	output.a = original.a;
	return output;
}
ENDCG

	}
}

Fallback off

}