// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AE/Wall"
{
	Properties
	{
		_Color_01("Color_01", Color) = (0.2180491,0.9433962,0.2187248,0)
		_Color_02("Color_02", Color) = (1,0.5801887,0.5801887,0)
		_Smoothness("Smoothness", Range( 0 , 2)) = 1.03
		_Normal_Power("Normal_Power", Range( 0 , 6)) = 0
		_Wall_01_A("Wall_01_A", 2D) = "white" {}
		_Wall_01_N("Wall_01_N", 2D) = "bump" {}
		_Wall_01_MS("Wall_01_MS", 2D) = "white" {}
		_Wall_Mask("Wall_Mask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Wall_01_N;
		uniform float4 _Wall_01_N_ST;
		uniform float _Normal_Power;
		uniform sampler2D _Wall_Mask;
		uniform float4 _Wall_Mask_ST;
		uniform float4 _Color_02;
		uniform sampler2D _Wall_01_A;
		uniform float4 _Wall_01_A_ST;
		uniform float4 _Color_01;
		uniform float _Smoothness;
		uniform sampler2D _Wall_01_MS;
		SamplerState sampler_Wall_01_MS;
		uniform float4 _Wall_01_MS_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Wall_01_N = i.uv_texcoord * _Wall_01_N_ST.xy + _Wall_01_N_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _Wall_01_N, uv_Wall_01_N ), _Normal_Power );
			float2 uv_Wall_Mask = i.uv_texcoord * _Wall_Mask_ST.xy + _Wall_Mask_ST.zw;
			float4 tex2DNode2 = tex2D( _Wall_Mask, uv_Wall_Mask );
			float2 uv_Wall_01_A = i.uv_texcoord * _Wall_01_A_ST.xy + _Wall_01_A_ST.zw;
			float4 tex2DNode1 = tex2D( _Wall_01_A, uv_Wall_01_A );
			o.Albedo = ( ( tex2DNode2 * _Color_02 * tex2DNode1 ) + ( tex2DNode1 * ( ( 1.0 - tex2DNode2 ) * _Color_01 ) ) ).rgb;
			float2 uv_Wall_01_MS = i.uv_texcoord * _Wall_01_MS_ST.xy + _Wall_01_MS_ST.zw;
			o.Smoothness = ( _Smoothness * tex2D( _Wall_01_MS, uv_Wall_01_MS ).a );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18702
-1920;11;1920;979;1685.42;548.8583;1.386506;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-1253.507,-776.3207;Inherit;True;Property;_Wall_Mask;Wall_Mask;7;0;Create;True;0;0;False;0;False;-1;9f69ce4e5d2735a49a5f50740adb2938;9f69ce4e5d2735a49a5f50740adb2938;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-1003.33,81.18478;Inherit;False;Property;_Color_01;Color_01;0;0;Create;True;0;0;False;0;False;0.2180491,0.9433962,0.2187248,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;7;-928.9729,-587.9088;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-1156.038,-146.9529;Inherit;True;Property;_Wall_01_A;Wall_01_A;4;0;Create;True;0;0;False;0;False;-1;e2833beffdb01d041b5e1d780f68dfe7;e2833beffdb01d041b5e1d780f68dfe7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-1271.875,-549.5865;Inherit;False;Property;_Color_02;Color_02;1;0;Create;True;0;0;False;0;False;1,0.5801887,0.5801887,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-535.813,36.08083;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-538.7982,-197.3108;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;13;-565.5508,468.6587;Inherit;True;Property;_Wall_01_MS;Wall_01_MS;6;0;Create;True;0;0;False;0;False;-1;56f37c8628dce30478794e560e3d15fd;56f37c8628dce30478794e560e3d15fd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-864.6089,732.274;Inherit;False;Property;_Normal_Power;Normal_Power;3;0;Create;True;0;0;False;0;False;0;1.88;0;6;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-287.1867,372.2379;Inherit;False;Property;_Smoothness;Smoothness;2;0;Create;True;0;0;False;0;False;1.03;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-243.4541,11.2901;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-65.60999,-190.7112;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;12;-557.1987,724.4317;Inherit;True;Property;_Wall_01_N;Wall_01_N;5;0;Create;True;0;0;False;0;False;-1;70f24e4014768144f9f1c7a71bb98ec1;70f24e4014768144f9f1c7a71bb98ec1;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;3.500504,498.287;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;481.0924,12.63807;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;AE/Wall;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;2;0
WireConnection;9;0;7;0
WireConnection;9;1;8;0
WireConnection;6;0;2;0
WireConnection;6;1;5;0
WireConnection;6;2;1;0
WireConnection;17;0;1;0
WireConnection;17;1;9;0
WireConnection;10;0;6;0
WireConnection;10;1;17;0
WireConnection;12;5;16;0
WireConnection;14;0;15;0
WireConnection;14;1;13;4
WireConnection;0;0;10;0
WireConnection;0;1;12;0
WireConnection;0;4;14;0
ASEEND*/
//CHKSM=431A1FE98AA81829CFFD607A2D0767B3AC69D7A5