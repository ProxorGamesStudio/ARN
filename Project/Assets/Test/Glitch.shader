// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32894,y:32624,varname:node_3138,prsc:2|emission-5768-OUT,alpha-1827-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31904,y:32320,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6845,x:32275,y:32573,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:node_6845,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4414-OUT;n:type:ShaderForge.SFN_Tex2d,id:9751,x:30641,y:32678,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_9751,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:9506,x:31097,y:33342,varname:node_9506,prsc:2;n:type:ShaderForge.SFN_Add,id:3750,x:31354,y:33290,varname:node_3750,prsc:2|A-5878-OUT,B-9506-TDB;n:type:ShaderForge.SFN_TexCoord,id:4298,x:31241,y:32845,varname:node_4298,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3652,x:31536,y:32878,varname:node_3652,prsc:2|A-4298-U,B-3750-OUT;n:type:ShaderForge.SFN_Append,id:4499,x:31874,y:32873,varname:node_4499,prsc:2|A-3652-OUT,B-4625-OUT;n:type:ShaderForge.SFN_Multiply,id:5878,x:31051,y:33134,varname:node_5878,prsc:2|A-2065-OUT,B-7120-OUT;n:type:ShaderForge.SFN_Vector1,id:7120,x:30844,y:33146,varname:node_7120,prsc:2,v1:3.14;n:type:ShaderForge.SFN_Slider,id:6746,x:31492,y:32456,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_6746,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Lerp,id:4414,x:32066,y:32561,varname:node_4414,prsc:2|A-4499-OUT,B-5014-UVOUT,T-6746-OUT;n:type:ShaderForge.SFN_Step,id:6050,x:32138,y:32763,varname:node_6050,prsc:2|A-9751-G,B-6746-OUT;n:type:ShaderForge.SFN_Multiply,id:1827,x:32428,y:32844,varname:node_1827,prsc:2|A-6845-A,B-9751-A,C-6050-OUT;n:type:ShaderForge.SFN_TexCoord,id:5014,x:31571,y:32560,varname:node_5014,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:1518,x:31336,y:33120,varname:node_1518,prsc:2|A-2065-OUT,B-9506-TDB;n:type:ShaderForge.SFN_Add,id:5786,x:31474,y:33046,varname:node_5786,prsc:2|A-4298-V,B-1518-OUT;n:type:ShaderForge.SFN_Sin,id:4625,x:31697,y:33033,varname:node_4625,prsc:2|IN-5786-OUT;n:type:ShaderForge.SFN_Tex2d,id:699,x:32288,y:33301,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_699,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:271f5ee3273dd7f4fae6e204d4f8c4bf,ntxv:0,isnm:False|UVIN-8431-OUT;n:type:ShaderForge.SFN_Multiply,id:5768,x:32721,y:32642,varname:node_5768,prsc:2|A-6845-RGB,B-4939-OUT;n:type:ShaderForge.SFN_Lerp,id:4939,x:32555,y:33064,varname:node_4939,prsc:2|A-699-RGB,B-6887-OUT,T-9620-OUT;n:type:ShaderForge.SFN_Vector1,id:6887,x:32438,y:33474,varname:node_6887,prsc:2,v1:1;n:type:ShaderForge.SFN_Append,id:7914,x:31658,y:33184,varname:node_7914,prsc:2|A-3652-OUT,B-5786-OUT;n:type:ShaderForge.SFN_Cos,id:8431,x:31852,y:33184,varname:node_8431,prsc:2|IN-7914-OUT;n:type:ShaderForge.SFN_Power,id:9620,x:32188,y:33004,varname:node_9620,prsc:2|VAL-6746-OUT,EXP-2837-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2837,x:31935,y:33086,ptovrint:False,ptlb:Ramp Power,ptin:_RampPower,varname:node_2837,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_RemapRange,id:2065,x:30928,y:32899,varname:node_2065,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9751-R;n:type:ShaderForge.SFN_Append,id:381,x:32065,y:33225,varname:node_381,prsc:2|A-8431-OUT,B-8284-OUT;n:type:ShaderForge.SFN_Cos,id:8284,x:31852,y:33313,varname:node_8284,prsc:2|IN-5786-OUT;proporder:7241-6845-9751-6746-699-2837;pass:END;sub:END;*/

Shader "Shader Forge/Glitch" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _MainTexture ("Main Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Ramp ("Ramp", 2D) = "white" {}
        _RampPower ("Ramp Power", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _Opacity;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform float _RampPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float node_2065 = (_Mask_var.r*2.0+-1.0);
                float4 node_9506 = _Time/10 + _TimeEditor;
                float node_3652 = (i.uv0.r+((node_2065*3.14)+node_9506.b));
                float node_5786 = (i.uv0.g+(node_2065+node_9506.b));
                float2 node_4414 = lerp(float2(node_3652,sin(node_5786)),i.uv0,_Opacity);
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(node_4414, _MainTexture));
                float2 node_8431 = cos(float2(node_3652,node_5786));
                float4 _Ramp_var = tex2D(_Ramp,TRANSFORM_TEX(node_8431, _Ramp));
                float node_6887 = 1.0;
                float3 emissive = (_MainTexture_var.rgb*lerp(_Ramp_var.rgb,float3(node_6887,node_6887,node_6887),pow(_Opacity,_RampPower)));
                float3 finalColor = emissive;
                return fixed4(finalColor,(_MainTexture_var.a*_Mask_var.a*step(_Mask_var.g,_Opacity)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
