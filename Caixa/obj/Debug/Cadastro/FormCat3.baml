   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa1    +Artebit.Restaurante.Caixa.Cadastro.FormCat3   5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   5      @d2http://schemas.microsoft.com/expression/blend/2008  
   5      $	��637��5      $	��411��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5
      $��Categoria 3��5      
�������&   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5   &    � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value*  $Telerik.Windows.Controls.RadGridView*  $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � $	��391��6    � ColumnDefinitions 5   
   �� 5      $
��420* �6   �� 5      $
��205* �6   .+�5   
   �� -   5        $��lbCategoria��5   e   $��Categoria 3��6   $��32��6&   $��Center=�62   $��258,0,60,356q�6O   $��Center=�6x   $��16(�6�   $
��Bold"�6�   ���#B68944$	��102��6�     5    
   -   5        $��	gridDados��5    K   $
��Left=�6   $��0,55,0,0q�69   $	��Top=�6\   $	��625��6t   $	��284��6�   ;	  5Telerik.Windows.Controls.GridView.GridViewDataControl 	  AutoGenerateColumns .  	 	  CanUserFreezeColumns	 .  
 	  RowIndicatorVisibility$
 	Collapsed=�6�    	  CanUserReorderColumns .   	  MinColumnWidth$ 50Q�6   	  ShowGroupPanel .   	  ShowGroupFooters .   	  SelectionMode$ Extended=�6K  $��2��6�   	  Columns 5!      3
  -Telerik.Windows.Controls.GridViewSelectColumn
  5"      1  +Telerik.Windows.Controls.GridViewDataColumn  5#      -  'Telerik.Windows.Controls.GridViewColumn   IsFilterable .     
IsReadOnly . 6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Produto��6�      Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ * 6�      DataMemberBinding 6-   �� *EPRODUTO1.nome+6�     5$      $ Preço��6-    .   . $	 100 6�      
UniqueName$ Preco��6�    6Q   �� *	nuPreco+   CellTemplate 5%      �� 5&      .R�5'      � .+�5(   "       Name$ 
ComboPreco��66   -   5        {�.  �� SelectedIndex 5(   y   �� *	nuPreco+.@�5)   &   .  (Telerik.Windows.Controls.RadComboBoxItem  $��-��6>     5*   &   $��Preço 1��6>     5+   &   $��Preço 2��6>     5,   &   $��Preço 3��6>   5-   #   5.      5/      51      53        54   
   -   5        $��btAdd��54   `   $��28��6   $��32��6(   $
��Left=�63   $��0,25,0,0q�6N   $	��Top=�6m   ��55      �� 56      $(��"pack://application:,,,/Img/add.png��6   $��20��6D   $��20��6P   $
��Left=�6[   $��1q�6v   58        59   
   -   5        $��	btExcluir��59   a   $��28��6   $��32��6(   $
��Left=�63   $��	34,25,0,0q�6N   $	��Top=�6r   ��5:      �� 5;      $2��,pack://application:,,,/Img/cancelarItens.png��6   $��20��6N   $��20��6Z   $
��Left=�6e   $��1q�6�   5=      �� 5>   
   $��Bottom=�6   
�������$	��625��6D   $��50��6P   $��2��6\   ._�5?      � .+�5@        -   5        $��	btnSalvar��5@   $   $��Salvar��65   $��	0 0 100 5q�6F   $��Right=�6Y   $��80��6u   $��25��6�      BotaoMenor2#��[  6�     5A      -   5        $��btnCancelar��5A   $   $��Cancelar��67   $��0 0 10 5q�6J   $��Right=�6\   $��80��6x   $��25��6�   #��[  6�   5B      5C      5D      5E      