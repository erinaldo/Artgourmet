   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormVendedor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5
   	   $	��350��5   	   $	��500��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5   	      CanMove . $��Vendedor��5   	   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5   	   
������ &   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5   	    � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value*  $Telerik.Windows.Controls.RadGridView* 
 $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5   
   � 
������� � RowDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       	 � ColumnDefinitions	 5"      �� 5#      �� 5$      $��3* �6#   �� 5%      $
��0.2* �6#   .+�5)      �� $��Cod Usuário:��6   $��0��6,   $��Center=�69   $��Center=�6V   �� 5*      $��Código:��6   $��1��6'   $��Center=�64   $��Center=�6Q   �� 5+      $��Nome:��6   $��2��6%   $��Center=�62   $��Center=�6O   �� 5,      $��
Comissão:��6   $��3��6)   $��Center=�66   $��Center=�6S   �� 5-      $��Ativo:��6   $��4��6&   $��Center=�63   $��Center=�6P     5.      -   5        $��comboCodigoUsu��5.   "   
 �� SelectedValuePath$
 
codusuario��68    �� DisplayMemberPath$ 
codusuario��6W   $��1��6v   $��25��6�   $��0,7,0,5q�6�   G� 5/      -   5        $��txboxCodigo��5/      $��1��6-   $��1��6=   $��25��6J   /	 
 )Telerik.Windows.Controls.RadMaskedTextBox	  50      -   5        $��	txboxNome��50   '   $��1��68   $��2��6H   $��25��6U    	  MaskType$
 None=�6a    	  Mask$ ######��6q   /
 
 )Telerik.Windows.Controls.RadNumericUpDown
  51      -   5        $��txboxComissao��51   '   $��1��6<   $��3��6L   $��25��6Y    
  ValueFormat$ Numeric=�6e   +  %Telerik.Windows.Controls.RadRangeBase   Maximum$	 100Q�6{    
  NumberDecimalDigits$ 2��6�   $
��Left=�6�   �� 52      -   5        $��
checkAtivo��52   O   $��1��6   $��4��6'   $��Center=�64   6O   �� 56   
   $��Bottom=�6   
�������$	��490��6D   $��50��6P   $��6��6\   $��3��6i   ._�57      � .+�58        -   5        $��	btnSalvar��58   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��6w   $��25��6�      BotaoMenor2#��[  6�     59      -   5        $��btnCancelar��59   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�6^   $��80��6z   $��25��6�   #��[  6�   5:      5;      5=      5>      