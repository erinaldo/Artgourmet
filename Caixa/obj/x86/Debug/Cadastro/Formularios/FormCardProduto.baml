   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa8    2Artebit.Restaurante.Caixa.Cadastro.FormCardProduto   5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   5      @d2http://schemas.microsoft.com/expression/blend/2008  
   5      -   5        $	��506��5   b   $	��392��6n   ) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5      ( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6*   .��5	      � $��10,10,10,10q�6    � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��label1��5   Z   $��Descrição :��6   $��28��6&   $
��Left=�62   $��0��6M   $��0��6h   $	��Top=�5      �� 5   
   -   5         ��Name$ txtDescricao��5   F   $��23��6   $
��Left=�6   $��0��69   $��1��6\   $	��Top=�5      $	��309��6*   ��66   �� *	descricao+�� 5   
   -   5         ��Name$ label1_Copy��5   F   $
��Cor:��6   $��28��6   $
��Left=�6+   $��0��6[   $	��Top=�5      $��1��6(   2  ,Telerik.Windows.Controls.RadColorPaletteView  5   
   -   5          Name$ 
colorFundo��5   &   $
��Left=�6:   $	��Top=�5   &   $��1��6>   $��1��6K    �� SelectedValue 5   U   �� *cor+5   K   �� 5!   
   -   5         ��Name$ chkAtivo��5!      $��Ativo��6%   $
��Left=�65   $��2��6P   $��0��6]   $��2��6m   $	��Top=�5"      	 d� 	IsChecked	 6+   �� *ativo+�� 5$   
   -   5        $ chkUsaPreco��5$      $��Associa Produto��6(   $
��Left=�6B   $��3��6]   $��0��6j   $��2��6z   $	��Top=�5%      	 6+   �� *
usaPreco+�� 5'   
   -   5        $ label1_Copy1��5'   J   $��Produto:��6   $��28��6#   $
��Left=�6/   $��0��6`   $	��Top=�5(      $��4��6(   *  $Telerik.Windows.Controls.RadComboBox  5*   
   -	   5        
  Name$
 
cbProdutos��5*         
IsEditable . $
��Left=�6D   $��1��6�   !   CanAutocompleteSelectItems$
 TrueX�5+         IsFilteringEnabled .   �� IsTextSearchCaseSensitive .   �� IsTextSearchEnabled . $	��300��5,   9    �� SelectedValuePath$ 	idProduto��6E    �� DisplayMemberPath$
 nome��6c   $	��Top=�5-         TextSearchMode$ Contains=�66   $��4��6P      Text$
 nome��6]    5*   _   �� *	idProduto+{�5+   |   �� *
usaPreco+5-   ]   �� 50   
   -
   5        $ label1_Copy2��50   H   $��Preço:��6   $��28��6!   $
��Left=�6-   $��0��6^   $	��Top=�51      $��5��6(     54   
   -   5        $
 cbPrecos��54      $
��Left=�60   $	��Top=�6K   $��1��6�   $	��145��55      $��5��6*    54   c   �� *	nuPreco+.@�56      �� $��Preço 1��6    � DataContext$ 1��6-   �� 57      $��Preço 2��6   $ 2��6-   �� 58      $��Preço 3��6   $ 3��6-   59      �� 5;   
   $��Bottom=�6   
�������$��40��6D   $��0,0,10,0q�6Q   $��2��6c   $��6��6w   ._�5<      � .+�5=      (  "Telerik.Windows.Controls.RadButton  -   5        $
��btOk��5=   l   $��OK��6$   $��25��61   $��Right=�6=   $��	0 5 100 0q�6Y   $	��Top=�5>   $   $��84��6<      BotaoMenor2#��[  6Z     5?      -   5        $��
btCancelar��5@   $   $��Cancelar��5?   $   $��25��67   $��Right=�6C   $��0 5 5 0q�6_   $	��Top=�5@   6   $��84��6N   #��[  6r   5A      5B      5D      5E      