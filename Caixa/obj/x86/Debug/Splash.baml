   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa&     Artebit.Restaurante.Caixa.Splash   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    64   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   @    � Title$ Splash��6e   $	��310��6t   $	��510��5   	    � WindowStyle$
 None=�6    � AllowsTransparency . $��Center=�6X   $��Center=�5   	    � 
ResizeMode$ NoResize=�6$   ��5   (   Z�  � 	Resources 5         showStoryBoard �� TargetProperty �� 
TargetName<  6System.Windows.Media.Animation.EasingThicknessKeyFrame p� KeyTime	 p� Value  HideStoryBoard��%�  &	        &	 �     �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��5   /   .	�5	        $ 0:0:0��6*   $	 0,0,0,0q�6:     5
      $ 0:0:1��6*   $	 0,0,0,70q�6:   5      5      �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��5   /   .	�5        $ 0:0:0��6*   $	 0,0,0,70q�6:     5      $ 0:0:1��6*   $	 	0,0,0,150q�6:   5      5      5      .��5      � .+�5   
   �� $��0,0,0,0q�6   $��Img/fundoSplash2.png��6!   $
��Fill=�6?   $	��500��6N   
 J� BitmapEffect
 5      F� 5       F� Color$ Black��6)    F� 	Direction$	 310Q�67    F� ShadowDepth$ 5Q�6G    F� Opacity$ .1Q�6W    F� Softness$ 1Q�6d   5      �� 5   
   $��5,32,16,116q�6   $��Img/logoSplash2.png��6%   $
��Fill=�6B   $	��162��6Q   $	��489��6^    J� RenderTransformOrigin$ 0.514,0.542&�5      � 5   
   $��Bottom=�6   $��Stretch=�6*   $	��150��6H   ���	#00FFFFFF J� OpacityMask 5       �� 5!       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5"      �  � Color$ 	#00000000��6#    � Offset$ 0.283Q�65   � 5#      $ 	#00000000��6#   $ 0.778Q�65   � 5$      $ White��6#   $ 0.491Q�61   5%      .+�5(      �� -   5         ��Name$ 
txtLoading��5(       �� TextAlignment$
 Left=�6,   $��16(�6A   $��Normal"�6O   ���#FFFFFF$��Beta 0.9��5)      $
��Wrap=�6(   $��Bottom=�6<   $��Right=�6W   $	��421��6s   $��Segoe UI)�5*       J� Effect 5+      3  -System.Windows.Media.Effects.DropShadowEffect  5,         
BlurRadius$ 6Q�6'      Color$ 	#FF797979��66      	Direction$	 201Q�6H      Opacity$
 0.68Q�6X      ShadowDepth$ 3Q�6g   5.      51      �� 52   
   -   5         ��Name$ 	txtVersao��52      $�� ��6#   $
��Left=�6.   $��435,272,0,0q�6I   $	��Top=�6^   $��Segoe UI)�53      $��15(�6&   
�������64   55      56      