   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa8    2Artebit.Restaurante.Caixa.Cadastro.FormCardProduto   5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   5      @d2http://schemas.microsoft.com/expression/blend/2008  
   5      -   5        $	��619��5      $	��392��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5
      $��Produtos��5      &   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 6    
������� � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value*  $Telerik.Windows.Controls.RadGridView*  $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � $	��609��6    � ColumnDefinitions 5   
   �� 5      $
��522* �6   �� 5      $
��170* �6   .+�5    
     -   5        $��	gridDados��5    K   $
��Left=�6   $��0,52,0,0q�69   $	��Top=�6\   $	��609��6t   $	��265��6�   ;	  5Telerik.Windows.Controls.GridView.GridViewDataControl 	  AutoGenerateColumns .  	 	  CanUserFreezeColumns	 .  
 	  RowIndicatorVisibility$
 	Collapsed=�6�    	  CanUserReorderColumns .   	  MinColumnWidth$ 50Q�6   	  ShowGroupPanel .   	  ShowGroupFooters .   	  SelectionMode$ Multiple=�6K  $��2��6d   	  CanUserInsertRows .   	  CanUserDeleteRows .   	  
IsReadOnly .  	  Columns 5!      3
  -Telerik.Windows.Controls.GridViewSelectColumn
  5"      -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ 30 6/   1  +Telerik.Windows.Controls.GridViewDataColumn  5#         IsFilterable .  6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Código��6h   $	 120 6x      DataMemberBinding 6-   �� *codigo+6x     5$       .  $ Produto��6e   $
 300* 6v    6-   �� *nome+6v     5%       .  $ Unidade��6i   $ 80 6z    6-   �� *
undVenda+6z   5'      �� 5(   
   -   5        $��label1��5(   ]   $��	Produto :��6   $��28��6$   $
��Left=�60   $��0,12,0,0q�6K   $	��Top=�6k   �� 5)   
   -   5        $��txtNomeProduto��5)   L   $��23��6   $
��Left=�6   $��	57,15,0,0q�69   $	��Top=�6b   $	��309��6z     5*   
   -   5        $��btBuscar��5*   h   $��Buscar��6   $��29��6-   $
��Left=�69   $��
382,12,0,0q�6T   $	��Top=�6x   $��53��6�      BotaoMenor2#��[  6�   �� 5,   
   $��Bottom=�6   
�������$	��610��6D   $��40��6P   $��2��6\   ._�5-      � .+�5.        -   5        $
��btOk��5.   l   $��OK��6$   $��25��61   $��Right=�6=   $��	0 5 100 0q�6Y   $	��Top=�6x   $��84��6�   #��[  6�     5/      -   5        $��
btCancelar��5/   p   $��Cancelar��6$   $��25��67   $��Right=�6C   $��0 5 5 0q�6_   $	��Top=�6�   $��84��6�   #��[  6�   50      51      53      54      