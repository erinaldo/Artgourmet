   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormVendedor   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      $	��350��5   K   $	��500��6X   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6d      CanMove . $��Vendedor��5      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6&   
������  � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter*  $Telerik.Windows.Controls.RadGridView* 
 $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5	      ��  �@ Theme$ Windows7 6;   5
      �� 5   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � 
������� � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      $��3* �6   �� 5      $
��0.2* �6   .+�5#   
   �� $��Cod Usuário:��6   $��0��6(   $��Center=�65   $��Center=�6R   �� 5$   
   $��Código:��6   $��1��6#   $��Center=�60   $��Center=�6M   �� 5%   
   $��Nome:��6   $��2��6!   $��Center=�6.   $��Center=�6K   �� 5&   
   $��
Comissão:��6   $��3��6%   $��Center=�62   $��Center=�6O   �� 5'   
   $��Ativo:��6   $��4��6"   $��Center=�6/   $��Center=�6L     5(   
   -   5        $��comboCodigoUsu��5(      	 �� SelectedValuePath$	 
codusuario��64   
 �� DisplayMemberPath$
 
codusuario��6S   $��1��5)      $��25��6.   $��0,7,0,5q�6:   G� 5*   
   -   5        $��txboxCodigo��5*      $��1��6)   $��1��69   $��25��6F   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5+   
   -   5        $��	txboxNome��5+   #   $��1��64   $��2��6D   $��25��6Q      MaskType$
 None=�6]      Mask$ ######��5,   #   /	 
 )Telerik.Windows.Controls.RadNumericUpDown	  5-   
   -   5        $��txboxComissao��5-   #   $��1��68   $��3��6H   $��25��6U    	  ValueFormat$ Numeric=�5.   #   +
  %Telerik.Windows.Controls.RadRangeBase 
  Maximum$	 100Q�69    	  NumberDecimalDigits$ 2��6G   $
��Left=�6_   �� 5/   
   -   5        $��
checkAtivo��5/   K   $��1��6   $��4��6#   $��Center=�60   6K   �� 52   
   $��Bottom=�6   
�������$	��490��6D   $��50��6P   $��6��6\   $��3��53      ._�54      � .+�55        -   5        $��	btnSalvar��55   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��56   $   $��25��6/      BotaoMenor2#��[  6S     57      -   5        $��btnCancelar��57   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�58   $   $��80��6@   $��25��6K   #��[  6q   59      5:      5<      5=      