   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa6    0Artebit.Restaurante.Caixa.Caixas.Janelas.Cliente   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      Jd2http://schemas.microsoft.com/expression/blend/2008	     	 
    5   J   Tmc;http://schemas.openxmlformats.org/markup-compatibility/2006	     	 
    5      $��Clientes��5   V   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6h   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5      $	��510��5   [   $	��414��6h   
������� � 	Resources 5      *  $Telerik.Windows.Controls.RadGridView �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter��%[   '        �� 5   
   "  6   .�5	      ��  �@ Theme$ Metro 6;   5
      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��150 �6   6+   �� 5      6   �� 5      $��35 �6   6*   .+�5   
   �� $��0��6   ._�5      � .+�5      �� $��
CPF/CNPJ :��6   $��40,25,285,0q�6-   $��13(�6B   $	��Top=�6P   �� 5      -   5        $��
txtCpfCnpj��5   U   $��23��6   $��Right=�6&   $��	0,28,96,0q�6B   $	��Top=�5      $	��190��62   �� 5      $��
Telefone :��6   $��40,60,285,0q�6-   $��13(�6B   $	��Top=�6P   �� 5      -   5        $��txtTelefone��5   U   $��23��6   $��Right=�6&   $��	0,63,96,0q�6B   $	��Top=�5      $	��190��62   (  "Telerik.Windows.Controls.RadButton  5      -   5        $��btnNovo��5   $   $
��Novo��63   $��
0 90 290 0q�6B   $��Right=�6V   $��80��5   $   $��25��6/      BotaoMenor2#��[  6Q     5       -   5        $��btnPesquisar��5    $   $��	Pesquisar��68   $��	0 90 30 0q�6L   $��Right=�5!   $   $��80��6@   $��25��6K   #��[  6r   5"      5#      �� 5%   
   $��1��6   ._�5&      � .+�5'        -   5          Name$ 	gridDados��5'   &   $	��520��69   $	��290��6I   $	��404��6V   $��0��6b   $��2��5(   &    J� Opacity$	 0.9Q�63   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl	   CanUserFreezeColumns	 .  
   RowIndicatorVisibility$
 	Collapsed=�6^      AutoGenerateColumns .     
IsReadOnly . $	��Top=�5)   8      ShowGroupPanel .     SelectionMode$ Single=�5*   &      Columns 5+      1  +Telerik.Windows.Controls.GridViewDataColumn  5,      6	  0Telerik.Windows.Controls.GridViewBoundColumnBase 	  Header$
 Nome��65   -
  'Telerik.Windows.Controls.GridViewColumn 
  Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ * 6n    
  IsGroupable .   
  IsFilterable .   	  DataMemberBinding 6C   �� *nomeFantasia+5-   I     5.      $ Telefone��65   $	 120 5/   5    .   .   5.   G   �� *
telefone+5/   U     50      $	 CPF��65   $	 120 6h    .  .   6B   �� *	cpfcnpj+51   H   53      54      55      �� 57   
   $��Bottom=�6   $��2��6,   
�������$	��400��6Q   $��35��6]   ._�58      � .+�59        -   5        $��btnOK��59   $   $��OK��61   $��	0 0 100 0q�6>   $��Right=�6Q   $��80��6m   $��25��5:   $   #��[  6D     5;      -   5        $��btnCancelar��5;   $   $��Cancelar��67   $��0 0 10 0q�6J   $��Right=�6\   $��80��5<   $   $��25��6/   #��[  6U   5=      5>      5?      5@      