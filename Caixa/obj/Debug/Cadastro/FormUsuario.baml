   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa4    .Artebit.Restaurante.Caixa.Cadastro.FormUsuario   5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5   	   $	��400��5   	   $	��600��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5   	      CanMove . $��Usuarios��5   	   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5   	   
������ &   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5	   "    � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value*  $Telerik.Windows.Controls.RadGridView*  $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��50 �6   ��5   
   �� 5      	 �� ImageSource$0	 *pack://application:,,,/Img/imagemFundo.png��6   .+�5   
   ,	  &Telerik.Windows.Controls.RadTabControl	  -   5        $��radTabControl1��5   I   $��Stretch=�6    $��0q�6>   $��Stretch=�6_   ;
  5Telerik.Windows.Controls.TabControl.RadTabControlBase
 
  BackgroundVisibility$
 	Collapsed=�6{   .@�5       )  #Telerik.Windows.Controls.RadTabItem  -   5        $��RadTabEditar��5    ]   $��Informações��6!   $��30��66   $
��Left=�6B   $	��Top=�6q   $	��120��6�   ���#EEEEEE.��5!      � $��0 15 0 0q�6    5"      �� 5#      �� 5$      �� 5%      �� 5&      �� 5'      �� 5(      $��3* �6(    � ColumnDefinitions 5*      �� 5+      �� 5,      $��2* �6+   .+�5.      �� $��	Usuário:��6    $��0��60   $��Center=�6=   $��Center=�6Z   $
��Bold"�6u   �� 5/      $��Nome:��6    $��1��6-   $��Center=�6:   $��Center=�6W   �� 50      $��Senha:��6    $��2��6.   $��Center=�6;   $��Center=�6X   �� 51      $��Ativo:��6    $��3��6.   $��Center=�6;   $��Center=�6X   /  )Telerik.Windows.Controls.RadMaskedTextBox  52      -   5        $��txtboxCodUsuario��52   /   $��1��6G   $��0��6W   $��25��6d      MaskType$
 None=�6p      EmptyContent$ Digite o código��6�   $	��270��6�   $
��Left=�6�     53      -   5        $��
txtboxNome��53   /   $��1��6A   $��1��6Q   $��25��6^   $
 None=�6j   $ Digite o nome��6z   $	��270��6�   $
��Left=�6�   G� 54      -   5        $��senha��54   "   $��1��6/   $��2��6?   $��25��6L   $	��270��6X   $
��Left=�6d   �� 55      -   5        $��checkUsuario��55   W   $��1��6   $��3��6/   $��Center=�6<   6W   56      57        58      -   5        $��RadTabAddMesa��58   X   $��Perfil��6!   $��30��61   $
��Left=�6=   $	��Top=�6m   $��5q�6�   $	��120��6�   ���#EEEEEE.��59      � -   5        $��	GridPefil��59      $��3q�6(    5:      �� 5;      $��35 �6(   �� 5<       5>      �� 5?      $	��171 �6+   �� 5@      $
��319* �6+   �� 5A      $��90 �6+   .+�5D      �� $��Sistema:��6    $
��Bold"�60   $��Center=�6B   $��Center=�6_   $��	2,10,0,10q�6z   �� 5E      $��Perfil:��6    $
��Bold"�6/   $��1��6B   $
��Left=�6R   $��Center=�6n   $��	5,10,0,10q�6�   �� 5F      $��Supervisor:��6    $
��Bold"�63   $��2��6F   $
��Left=�6V   $��Center=�6r   �� 5H      $��1��6#   $��3��60   .��5I      � -	   5        $��
gridScroll��5I       5J      �� 5K   "   �� 5L   "   $��2* �63   �� 5M   "   $��40 �63   5O      5P      5Q      5R      5S      �� 5T   
   $��Bottom=�6   $��1��6,   
�������$	��590��6Q   $��55��6]   ._�5U      � .+�5V        -
   5        $��	btnSalvar��5V   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��6w   $��25��6�      BotaoMenor2#��[  6�     5W      -   5        $��btnCancelar��5W   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�6^   $��80��6z   $��25��6�   #��[  6�   5X      5Y      5Z      5[      