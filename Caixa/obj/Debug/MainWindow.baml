   M S B A M L   `   `   `  ����e  aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7�Zclr-namespace:Telerik.Windows.Controls.TransitionEffects;assembly=Telerik.Windows.Controls*Telerik.Windows.Controls.TransitionEffects  5   	    Artebit.Restaurante.Caixa*   $Artebit.Restaurante.Caixa.MainWindow   5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	          	 �TransitionEffectsZclr-namespace:Telerik.Windows.Controls.TransitionEffects;assembly=Telerik.Windows.Controls	          	 5   	   R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5   	   -   5          � Title$  
MainWindow��5   	    � ShowInTaskbar .   � WindowStyle$
 None=�5   	    � WindowState$ 	Maximized=�5	   	   ���	#FFE1E6F1 J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ GoToPage��5      5        5   
   -   5        $
 Find��5      5       J� InputBindings 5      �� 5   
    �� Command$ GoToPage��6   
 �� Key$
 Home��6(   	 �� 	Modifiers$		 Altf�64   
 �� CommandParameter$

 Home��6D   .��5      � -   5        $��janela��5      $
��1000��6   $	��740��6)   $��Center=�69   $��Center=�6V    � RowDefinitions 5   
   �� 5      $��55��6   �� 5      $��48��6   �� 5      $	��30* �6    � ColumnDefinitions 5    
   �� 5!      $��3* �6   .+�5%   
   ��  �� CornerRadius$ 5��5&      ���#49181A$��1q�5'      $��-2q�5(      $��1��5)      $��3��6!    J� Effect 5*      3  -System.Windows.Media.Effects.DropShadowEffect  5+         ShadowDepth$ 0Q�6#      
BlurRadius$ 10Q�63   ._�5/      �� $ 5��50      
������ 
�������$��1q�53      }�. ._�55      � .+�56      �� -   5        $��btIncio��56      $��25��6,   $��88��68   $
��Left=�6C   $
��Left=�6{   $	��Top=�6�   $��0,80,0,0q�6�   $��0q�6�      BotaoMenor3#��[  6�   .��57      �  58      �� 59   "   �� 5:   "   .+�5<      �� $)��#pack://application:,,,/Img/home.png��6$   $��18��6Q   $��20��6]   $
��Left=�6h   $��10 0 0 0q�6�   �� 5=      $�� Início��6(   $
��Left=�67   $��1��6R   $��	6,4,-51,1q�6b   5>      5?      �� 5B      -   5        $��btMenuFiscal��5B      $��25��61   $	��110��6=   $
��Left=�6I   $
��Left=�6d   $��	90,80,0,0q�6�   $	��Top=�6�   #��[  6�   .��5C      �  5E      �� 5F   "   $��33 �63   �� 5G   "   $	��25* �63   .+�5I      �� $��0��6$   $/��)pack://application:,,,/Img/menuFiscal.png��64   $��20��6g   $��20��6s   $
��Left=�6~   $��10 0 0 0q�6�   �� 5J      $��1��6(   $��Menu Fiscal��68   $��Center=�6K   $��0 4 0 0q�6f   5K      5L      �� 5O      -   5        $��btMapaMesas��5O      $��25��60   $	��130��6<   $
��Left=�6H   $
��Left=�6c   $��
202,80,0,0q�6�   $	��Top=�6�   #��[  6�   .��5P      �  5R      �� 5S   "   $��33 �63   �� 5T   "   $	��25* �63   .+�5V      �� $��0��6$   $+��%pack://application:,,,/Img/opcoes.png��64   $��20��6c   $��20��6o   $
��Left=�6z   $��10 0 0 0q�6�   �� 5W      $��1��6(   $��Mapa de Mesas��68   $��Center=�6M   $��0 4 0 0q�6h   5X      5Y      �� 5]      -   5        $��btVoltar��5]      $��25��6-   $��65��69   $��Right=�6D   $��Center=�6`   $��4��6�   $��GoToPage��5^      $
��Home��6(   $	��Top=�6@   $��0,80,0,0q�6X   #��[  6j   .��5_      �  5`      �� 5a   "   �� 5b   "   .+�5d      �� $)��#pack://application:,,,/Img/sair.png��6$   $��20��6Q   $��20��6]   $
��Left=�6h   $��1q�6�   �� 5e      $��Voltar��6(   $��Center=�66   $��1��6Q   $��1,1,0,1q�6a   5f      5g      �� 5h      -	   5        $��btSair��5h      $��25��6+   $��65��67   $��Right=�6B   $��Center=�6^   $��4��6�   $	��Top=�5i   *   $��0,80,0,0q�6B   #��[  6T   .��5j      �  5k      �� 5l   "   �� 5m   "   .+�5o      �� $)��#pack://application:,,,/Img/sair.png��6$   $��20��6Q   $��20��6]   $
��Left=�6h   $��1q�6�   �� 5p      $
��Sair��6(   $��Center=�64   $��1��6O   $��1,1,0,1q�6_   5q      5r      5u      5w      ��5x      �� 5y       �� ImageSource$ /Img/imagemFundo.png��6   5{      � 5~   
   $��7��5      $��2��5�      $��70��5�      $��	-5 0 -9 0q�5�      $��Center=�5�      ��5�      �� 5�      $ /Img/fundoTopoWPF.png��6    j� Stretch$
 Fill=�6A   .+�5�      � $��Center=�5�      .+�5�      �� $��Img/logo1.png��5�      $
��Left=�5�      $��5 0 0 0q�5�      $��Center=�5�      $��55��5�      �� 5�      -
   5         ��Name$ TituloSistema��5�      $��Caixa��5�      
�������$��30(�5�      $	��577��5�      $
��Left=�5�      $��10 0 0 0q�5�      $��Center=�5�      5�      5�      � 5�      $��1��6   $��2��6   $��5 10 5 5q�6(    5�   
   �� 5�      $
��200* �6   .+�5�   
   �� �� 5�   
   -   5         ��Name$ 	principal��5�      $	��200��6,   5�      5�      5�      5�      