   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa8    2Artebit.Restaurante.Caixa.Cadastro.FormCardProduto   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   @d2http://schemas.microsoft.com/expression/blend/2008  
   -   $	��506��$	��392��) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�.��� $��10,10,10,10q� � RowDefinitions �� �� �� �� �� �� ��  � ColumnDefinitions �� $	��100 ��� $��* �.+��� -   $��label1��$��Descrição :��$��28��$
��Left=�$��0��$��0��$	��Top=��� -    ��Name$ txtDescricao��$��23��$
��Left=�$��0��$��1��$	��Top=�$	��309������ *	descricao+�� -    ��Name$ label1_Copy��$
��Cor:��$��28��$
��Left=�$��0��$	��Top=�$��1��2  ,Telerik.Windows.Controls.RadColorPaletteView  -     Name$ 
colorFundo��$
��Left=�$	��Top=�$��1��$��1�� �� SelectedValue �� *cor+�� -    ��Name$ chkAtivo��$��Ativo��$
��Left=�$��2��$��0��$��2��$	��Top=�	 d� 	IsChecked	 �� *ativo+�� -   $ chkUsaPreco��$��Associa Produto��$
��Left=�$��3��$��0��$��2��$	��Top=�	 �� *
usaPreco+�� -   $ label1_Copy1��$��Produto:��$��28��$
��Left=�$��0��$	��Top=�$��4��*  $Telerik.Windows.Controls.RadComboBox  -	   
  Name$
 
cbProdutos��   
IsEditable . $
��Left=�$��1��!   CanAutocompleteSelectItems$
 TrueX�   IsFilteringEnabled .   �� IsTextSearchCaseSensitive .   �� IsTextSearchEnabled . $	��300�� �� SelectedValuePath$ 	idProduto�� �� DisplayMemberPath$
 nome��$	��Top=�   TextSearchMode$ Contains=�$��4��   Text$
 nome�� �� *	idProduto+{��� *
usaPreco+�� -
   $ label1_Copy2��$��Preço:��$��28��$
��Left=�$��0��$	��Top=�$��5��  -   $
 cbPrecos��$
��Left=�$	��Top=�$��1��$	��145��$��5�� �� *	nuPreco+.@��� $��Preço 1�� � DataContext$ 1���� $��Preço 2��$ 2���� $��Preço 3��$ 3���� $��Bottom=�
�������$��40��$��0,0,10,0q�$��2��$��6��._�� .+�(  "Telerik.Windows.Controls.RadButton  -   $
��btOk��$��OK��$��25��$��Right=�$��	0 5 100 0q�$	��Top=�$��84��   BotaoMenor2#��[    -   $��
btCancelar��$��Cancelar��$��25��$��Right=�$��0 5 5 0q�$	��Top=�$��84��#��[  