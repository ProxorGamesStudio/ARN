// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Neuron/Hologram"
{
	Properties
	{
		_ColorFr ("Color Fresnel", Color) = (1,1,1,1)
		_Power ("Color Power", float) = 1
		//_MainTex ("MaskTexture", 2D) = "white" {}
		_PsedoPointLightPos("PseudoLight Pos", Vector) = (0,-10,0,1)
		_PsedoPointLightColor("PseudoLight Color", Color) = (1,1,1,1)
		_PsedoPointLightPos2("PseudoLight Pos", Vector) = (0,-10,0,1)
		_PsedoPointLightColor2("PseudoLight Color", Color) = (1,1,1,1)
		_EdgesMask("Edges Mask",2D) = "black" {}
		_MaskIntencity("Edges Mask Intencity", Range(0,2)) = 1
		_EdgesColor("Edges Color", Color) = (1,1,1,1)
		
		_ShadowColor ("Shadow's Color", Color) = (0,0,0,1)

		_TotalAlpha ("Alpha", Range(0,1)) = 1
		
		_Dist("Dist",Float) = 0
		_Height("Height", Float) = 0

		
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Overlay" "IgnoreProjector"="True" }
		LOD 300
		//MinusSrcAlpha

		Pass
		{
		blend SrcAlpha One
		ZWrite Off

		ColorMask RGB
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

			//sampler2D _MainTex;
		//	float4 _MainTex_ST;
			sampler2D _EdgesMask;
			float4 _EdgesMask_ST;
			fixed4 _ColorFr;
			fixed _Power;
			float4 _PsedoPointLightPos;
			fixed4 _PsedoPointLightColor;
			float4 _PsedoPointLightPos2;
			fixed4 _PsedoPointLightColor2;
			float _MaskIntencity;
			fixed4 _EdgesColor;
			fixed _TotalAlpha;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float4  posWorld = mul(unity_ObjectToWorld, v.vertex);
				float3 viewDirection = normalize(
               _WorldSpaceCameraPos - posWorld);
			    float3 normalDirection = UnityObjectToWorldNormal(v.normal);
								  
				float NdotV = max(0.0,dot( normalDirection, viewDirection ));
				 
				float3 specularReflection = FresnelLerpFast (half3(0,0,0), _ColorFr, NdotV);
				
				float3 pointToVertex = _PsedoPointLightPos.xyz - posWorld;
				float3 verticalGradient =1/dot(pointToVertex,pointToVertex);
				
				pointToVertex = _PsedoPointLightPos2.xyz - posWorld;
				float3 verticalGradient2 =1/dot(pointToVertex,pointToVertex);

				
				
				o.color = 
				_ColorFr * _Power 
				+ float4(specularReflection,1)+
				float4(verticalGradient,1)*_PsedoPointLightColor*_PsedoPointLightPos.w
				+ float4(verticalGradient2,1)*_PsedoPointLightColor2*_PsedoPointLightPos2.w
				;
				o.uv = v.uv;
				o.uv = TRANSFORM_TEX(v.uv, _EdgesMask);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{	
				fixed4 edgesMask = tex2D(_EdgesMask, i.uv);
				return fixed4((i.color + _MaskIntencity*_EdgesColor*edgesMask).rgb,_TotalAlpha);
			}
			ENDCG
		}
		
		
		
/*		 Pass {   
        Tags { "RenderType"="Transparent" "Queue"="Overlay" "IgnoreProjector"="True" }
            // rendering of projected shadow
       //  Offset -1.0, -2.0 
            // make sure shadow polygons are on top of shadow receiver
		 Cull Off
	//	 ZWrite Off
		 Blend SrcAlpha One
		 

         CGPROGRAM
 
         #pragma vertex vert 
         #pragma fragment frag
 		 #pragma fragmentoption ARB_precision_hint_fastest
         #include "UnityCG.cginc"
		 #include "Lighting.cginc"
 

        uniform fixed4 _ShadowColor;
		uniform float _Dist;
		uniform float _Height;
		 
		struct appdata
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 uv : TEXCOORD0;
		};
		
		struct v2f
		{
			float4 vertex : SV_POSITION;
			float3 normal: NORMAL;
			fixed4 color : COLOR;
			fixed heightFactor : TEXCOORD0;
		};
 
         v2f vert(appdata v)
         {
            v2f o;
			float4  posWorld = mul(_Object2World, v.vertex);
			o.heightFactor = step(_Height, posWorld.y);
			o.vertex = o.heightFactor*mul(UNITY_MATRIX_MVP, 
			v.vertex
			+ 
			(_Dist+v.uv.r*1.2+v.uv.g*2.4)*fixed4(v.normal.xyz,0)
			) ;
			o.normal = v.normal;
			return o;
         }
 
         fixed4 frag(v2f i) : SV_Target 
         {
			clip(i.heightFactor - 0.01);
            return _ShadowColor;
         }
 
         ENDCG 
      }*/
		
		
		
		
	}
}
