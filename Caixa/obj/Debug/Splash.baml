   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa&     Artebit.Restaurante.Caixa.Splash   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   @    � Title$ Splash��5   	   $	��310��6   $	��510��6%    � WindowStyle$
 None=�61    � AllowsTransparency . $��Center=�6t   $��Center=�6�    � 
ResizeMode$ NoResize=�6�   ��6D   Z�  � 	Resources 5         showStoryBoard �� TargetProperty �� 
TargetName<  6System.Windows.Media.Animation.EasingThicknessKeyFrame p� KeyTime	 p� Value  HideStoryBoard��%�  &	        &	 �     �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��5      .	�5	        $ 0:0:0��6*   $	 0,0,0,0q�6:     5
      $ 0:0:1��6*   $	 0,0,0,70q�6:   5      5      �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��5      .	�5        $ 0:0:0��6*   $	 0,0,0,70q�6:     5      $ 0:0:1��6*   $	 	0,0,0,150q�6:   5      5      5      .��5      � .+�5   
   �� $��0,0,0,0q�6   $��Img/fundoSplash2.png��6!   $
��Fill=�6?   $	��500��6N   
 J� BitmapEffect
 5      F� 5       F� Color$ Black��6-    F� 	Direction$	 310Q�6;    F� ShadowDepth$ 5Q�5   )    F� Opacity$ .1Q�69    F� Softness$ 1Q�6F   5      �� 5   
   $��5,32,16,116q�6   $��Img/logoSplash2.png��6%   $
��Fill=�6B   $	��162��6Q   $	��489��6^    J� RenderTransformOrigin$ 0.514,0.542&�6j   � 5   
   $��Bottom=�6   $��Stretch=�6*   $	��150��6H   ���	#00FFFFFF J� OpacityMask 5       �� 5!       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5"      �  � Color$ 	#00000000��6#    � Offset$ 0.283Q�65   � 5#      $ 	#00000000��6#   $ 0.778Q�65   � 5$      $ White��6#   $ 0.491Q�61   5%      .+�5(      �� -   5         ��Name$ 
txtLoading��5(       �� TextAlignment$
 Left=�6,   $��16(�5)      $��Normal"�6   ���#FFFFFF$��Beta 0.9��5*      $
��Wrap=�6    $��Bottom=�64   $��Right=�5+      $	��421��6+   $��MicrogrammaDMedExt)�67    J� Effect 5,      3  -System.Windows.Media.Effects.DropShadowEffect  5-         
BlurRadius$ 6Q�6      Color$ 	#FF797979��6+      	Direction$	 201Q�6=      Opacity$
 0.68Q�6M      ShadowDepth$ 3Q�6\   5/      52      �� 53   
   -   5         ��Name$ 	txtVersao��53      $�� ��6#   $
��Left=�6.   $��423,272,0,0q�6I   $	��Top=�6^   $��MicrogrammaDMedExt)�6v   $��15(�6�   
�������6�   55      56      