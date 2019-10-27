Shader "Custom/VisualEffectShader"{
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
     
        [Enum(Equal,3,NotEqual,6)] _StencilTest ("Stencil Test", int) = 6
    }
  
  
  
    SubShader {
 
        Tags { "Queue"="Transparent""RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
       
        Color  [_Color] 
        Stencil{
            Ref 1
            Comp [_StencilTest]
        }
      
    Pass {
    }
      
      
    }    
}