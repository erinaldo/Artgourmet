   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa/    )Artebit.Restaurante.Caixa.Caixas.Desconto   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5   	   Jd2http://schemas.microsoft.com/expression/blend/2008	     	 
    5   g   Tmc;http://schemas.openxmlformats.org/markup-compatibility/2006	     	 
    6�   &   Telerik.Windows.Controls.Theming    Theme-  'Telerik.Windows.Controls.ThemeConverter$  Transparent 5   K   $��Desconto��5   	   )  #Telerik.Windows.Controls.WindowBase   
ResizeMode$ NoResize=�6   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�61   $	��210��6�   $	��414��6�   
������� � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value*  $Telerik.Windows.Controls.RadGridView* 
 $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5	      �� 5
   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � .+�5      �� $��
19,32,0,37q�6   $;��5/Artebit.Restaurante.Caixa;component/Img/dinheiro.png��6!   $
��Fill=�6`   $
��Left=�6o   $	��107��6�   �� 5   
   -   5        $��label1��5   `   $��
Desconto :��6   $��28��6%   $
��Left=�61   $��
135,45,0,0q�6L   $	��Top=�6n   /	 
 )Telerik.Windows.Controls.RadNumericUpDown	  5   
   -   5        $��txtDesconto��5   8   $��140,69,88,0q�6#   $	��Top=�6K    	  NumberDecimalDigits$ 2��6c   +
  %Telerik.Windows.Controls.RadRangeBase 
  Minimum$ 0Q�6{   	 
  Maximum$	 20000Q�6�   
 	  ValueFormat$
 Currency=�6�   �� 5   
   $��Bottom=�6   $��1��6,   
�������$	��400��6Q   $��35��6]   ._�5      � .+�5        -   5        $��btnOK��5   $   $��OK��61   $��	0 0 100 0q�6>   $��Right=�6Q   $��80��6m   $��25��6x      BotaoMenor2#��[  6�     5      -   5        $��btnCancelar��5   $   $��Cancelar��67   $��0 0 10 0q�6J   $��Right=�6\   $��80��6x   $��25��6�   #��[  6�   5      5      5      5      