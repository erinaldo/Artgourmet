   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormVendedor   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    $	��350��$	��500��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�   CanMove . $��Vendedor��(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�
������  � 	Resources (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter*  $Telerik.Windows.Controls.RadGridView* 
 $Telerik.Windows.Controls.RadComboBox��%�   '        '  /     '  [     �� "  .���  �@ Theme$ Windows7 �� "  .���  �@ Theme$ Metro �� "  .���  �@ Theme$ Summer .��� 
������� � RowDefinitions �� �� �� �� �� ��  � ColumnDefinitions �� �� $��3* ��� $
��0.2* �.+��� $��Cod Usuário:��$��0��$��Center=�$��Center=��� $��Código:��$��1��$��Center=�$��Center=��� $��Nome:��$��2��$��Center=�$��Center=��� $��
Comissão:��$��3��$��Center=�$��Center=��� $��Ativo:��$��4��$��Center=�$��Center=�  -   $��comboCodigoUsu��	 �� SelectedValuePath$	 
codusuario��
 �� DisplayMemberPath$
 
codusuario��$��1��$��25��$��0,7,0,5q�G� -   $��txboxCodigo��$��1��$��1��$��25��/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   $��	txboxNome��$��1��$��2��$��25��   MaskType$
 None=�   Mask$ ######��/	 
 )Telerik.Windows.Controls.RadNumericUpDown	  -   $��txboxComissao��$��1��$��3��$��25�� 	  ValueFormat$ Numeric=�+
  %Telerik.Windows.Controls.RadRangeBase 
  Maximum$	 100Q� 	  NumberDecimalDigits$ 2��$
��Left=��� -   $��
checkAtivo��$��1��$��4��$��Center=��� $��Bottom=�
�������$	��490��$��50��$��6��$��3��._�� .+�  -   $��	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[  