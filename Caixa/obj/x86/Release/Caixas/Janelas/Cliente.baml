   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa6    0Artebit.Restaurante.Caixa.Caixas.Janelas.Cliente   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    Jd2http://schemas.microsoft.com/expression/blend/2008	     	 
    Tmc;http://schemas.openxmlformats.org/markup-compatibility/2006	     	 
    $��Clientes��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�$	��510��$	��414��
������� � 	Resources *  $Telerik.Windows.Controls.RadGridView �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter��%6   '        �� "  .���  �@ Theme$ Metro .���  � RowDefinitions �� $	��150 ��� �� $��35 �.+��� $��0��._�� .+��� $��
CPF/CNPJ :��$��40,25,285,0q�$��13(�$	��Top=��� -   $��
txtCpfCnpj��$��23��$��Right=�$��	0,28,96,0q�$	��Top=�$	��190���� $��
Telefone :��$��40,60,285,0q�$��13(�$	��Top=��� -   $��txtTelefone��$��23��$��Right=�$��	0,63,96,0q�$	��Top=�$	��190��(  "Telerik.Windows.Controls.RadButton  -   $��btnNovo��$
��Novo��$��
0 90 290 0q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -   $��btnPesquisar��$��	Pesquisar��$��	0 90 30 0q�$��Right=�$��80��$��25��#��[  �� $��1��._�� .+�  -     Name$ 	gridDados��$	��520��$	��290��$	��404��$��0��$��2�� J� Opacity$	 0.9Q�;  5Telerik.Windows.Controls.GridView.GridViewDataControl	   CanUserFreezeColumns	 .  
   RowIndicatorVisibility$
 	Collapsed=�   AutoGenerateColumns .     
IsReadOnly . $	��Top=�   ShowGroupPanel .     SelectionMode$ Single=�   Columns 1  +Telerik.Windows.Controls.GridViewDataColumn  6	  0Telerik.Windows.Controls.GridViewBoundColumnBase 	  Header$
 Nome��-
  'Telerik.Windows.Controls.GridViewColumn 
  Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ *  
  IsGroupable .   
  IsFilterable .   	  DataMemberBinding �� *nomeFantasia+  $ Telefone��$	 120  .   .   �� *
telefone+  $	 CPF��$	 120  .  .   �� *	cpfcnpj+�� $��Bottom=�$��2��
�������$	��400��$��35��._�� .+�  -   $��btnOK��$��OK��$��	0 0 100 0q�$��Right=�$��80��$��25��#��[    -   $��btnCancelar��$��Cancelar��$��0 0 10 0q�$��Right=�$��80��$��25��#��[  