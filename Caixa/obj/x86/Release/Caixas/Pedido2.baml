   M S B A M L   `   `   `  ���� T  PArtebit.Restaurante.Caixa, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Artebit.Restaurante.Caixa.Controles#Artebit.Restaurante.Caixa.Controles  n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7�Zclr-namespace:Telerik.Windows.Controls.GridView;assembly=Telerik.Windows.Controls.GridView!Telerik.Windows.Controls.GridView e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7eHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.ControlsTelerik.Windows.Controls .    (Artebit.Restaurante.Caixa.Caixas.Pedido2   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 Q	Controles1clr-namespace:Artebit.Restaurante.Caixa.Controles	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   @d2http://schemas.microsoft.com/expression/blend/2008  
   vtelerikGridViewZclr-namespace:Telerik.Windows.Controls.GridView;assembly=Telerik.Windows.Controls.GridView  
   ]ControlsHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls  
   
������ .���� .+�� .+�4   .Artebit.Restaurante.Caixa.Controles.Cabecalho1  -      Name$  	Cabecalho��   Titulo$  ��   BotaoInicioEvento .  � -    �Name$ 
GridGlobal��$	��990��$	��650��$��10,5,0,0q� � ColumnDefinitions �� $	��150 ��� $	��440 ��� $	��200 ��� $	��200 � � RowDefinitions �� $	��247 ��� $	��168 ��� $	��144 ��� $��87 �.+�� $��0��$��2��$��0��$��2��$��0 0 20 0q� � 	Resources    RowStyle �� 
TargetType3  -Telerik.Windows.Controls.GridView.GridViewRow �� Property	 �� Value
 �� 
TargetType ��Name J� Opacity ��Name �� StringFormat �� Triggers)  #Telerik.Windows.Controls.RadRowItem P� Property P� Value �� 
TargetName  Headerstyle9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   ShadowDepth   Opacity  Footerstyle9  3Telerik.Windows.Controls.GridView.GridViewFooterRow ��Name �� Path �� RelativeSource �� AncestorType��%^	  &	        &	 �    &	 �    �� "  .���  � ��$	 27����  � ��	 �� "
  .R��� $ borda1��
�������$	 0.8Q�._�� $ SelectedRow�� �� $��51 ��� $	��289 ��� $��60 ��� $��60 ��� $��80 �.+��� ���#49181A$��0q�$��0��$	 0.8Q�._��� $	 chk��-   $��Center=�$��Center=����� *nuItem+�� ���#49181A$��1q�$��1��._��� $��5 0 0 0q�$��Center=����� *nome+�� ���#49181A$��1q�$��2��._��� $��Center=�$��Center=����� *und+�� ���#49181A$��1q�$��3��._��� $��Center=�$��Center=����� *qtd+�� ���#49181A$��1q�$��4��._��� $��Center=�$��Center=����� *preco+$ {0:c}�� P�  �@ 
IsSelected . . ��� $ borda1�� � ��
	 � ����� "  .���  � ��$	 33����  � ��	 �� "
  .R��� $ borda1�����#B68944���#49181A$��0 0 0 3q�   $ Black��$ 5Q�$	 0.9Q�._��  �� $	��340 ��� $��60 ��� $��60 ��� $��80 �.+��� ���#49181A$��1q�$��0��._��� $��35 0 0 0q�
�������$��Center=�$��14(�$��Produto���� ���#49181A$��1q�$��1��._��� $��Center=�
�������$��Center=�$��14(�$	��Und���� ���#49181A$��1q�$��2��._��� $��Center=�
�������$��Center=�$��14(�$	��Qtd���� ���#49181A$��1q�$��3��._��� $��Center=�$��Center=�
�������$��14(�$��Preço (R$)���� "  .���  � ��$	 27����  � ��	 �� "
  .R��� $ borda1�����#6B4523���#49181A$��0 2 0 0q�   $ Black��$ 5Q�$	 0.9Q�._��  �� $��51 ��� $��28 ��� $	��201 ��� $	��120 ��� $��59 ��� $��2 ��� $��80 �.+��� ���#49181A$��0q�$��0��$	 0.8Q�._��� $ 	chk_total��-   $��Center=�$��Center=��� $��0q�$��1��._��� $ nupedido��$��5 0 0 0q�
�������$��Center=�$��Right=�$��13(����� $ Pedidos� �� $ telerik:RadGridViewL��� $��0q�$��2��._��� 
�������$��Center=�$
��Left=�$��13(�$��Pedidos���� $��0q�$��3��._��� $��Right=�
�������$��Center=�$��13(�$��Total de produtos:���� $��0q�$��4��._��� $
 cont��$��Center=�
�������$��Center=�$��13(�$�� ���� ���#49181A$��2q�$��5���� $��0q�$��6��._��� $ totpreco��$��Center=�
�������$��Center=�$��13(�$�� ��.+�/  )Telerik.Windows.Controls.RadBusyIndicator  -     Name$ busyIndicator��.��*  $Telerik.Windows.Controls.RadGridView  -     Name$ 	gridDados��;	  5Telerik.Windows.Controls.GridView.GridViewDataControl 	  SelectionMode$ Single=�$	��570��
������  	  CanUserFreezeColumns .   	  RowIndicatorVisibility$ 	Collapsed=�  	  AutoGenerateColumns  .  ! 	  
IsReadOnly! . " 	  ShowGroupPanel" .  # 	  SelectionUnit$# FullRow=�$
��Left=�$��0q�
������ $ 	  RowStyle#$ [  % 	  HeaderRowStyle#% [ & 	  FooterRowStyle#& [ ' 	  ShowColumnFooters' . $��Visible=�$��Disabled=�( 	  Columns( 3
  -Telerik.Windows.Controls.GridViewSelectColumn
  1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase)   Header$) Cód.��-  'Telerik.Windows.Controls.GridViewColumn*   IsGroupable* .  +   	IsVisible+ .  ,   IsFilterable, .  -   DataMemberBinding- �� *nuItem+.   AggregateFunctions.   $) Descrição��* . , . - �� *nome+  $) Preço��* . , . /   DataFormatString$/ {0:c}��- �� *preco+. �� $��2��$��2��$��0��$��4��$��0 3 0 17q�  
BordaClara#��[ ._��  �� $	��100 ��� $��40 ��� $��3* �.+��� -   $��label3��$��Pedidos��$��27��$
��Left=�$	��Top=�$��13(�$��5 3 0 0q�� $��0��$��0 40 0 0q�.+��� $��	20 0 15 0q�$	��210��$
��Left=�.+��� $��Produto:��$
��Left=�$��Center=�*  $Telerik.Windows.Controls.RadComboBox  -   0  Name$0 
cmbProduto��1   
IsEditable1 . $
��Left=�!2   CanAutocompleteSelectItems$
2 TrueX�3   IsFilteringEnabled3 .  4 �� IsTextSearchCaseSensitive4 .  5 �� IsTextSearchEnabled5 . $	��210��6 �� SelectedValuePath$6 	idProduto��7 �� DisplayMemberPath$7 DescricaoPrd��$��Center=�8   TextSearchMode$8 Contains=�9   OpenDropDownOnFocus9 . : � DataContext: �� �� .+��� Quantidade:/  )Telerik.Windows.Controls.RadMaskedTextBox  -	   ;  Name$; txtQtd��$
��Left=�<   MaskType$< Numeric=�=   Mask$= D��$��25��$��50��>   Value$> 1���� -
   ? ��Name$
? btOk��$��	10 10 0 0q�  BotaoPadrao#��[ $��OK��$��40��$��30���� $��1��$��Center=�$��Center=�.+��� -   $ chkAdicional��$��Adicional / Guarnição��$y�Hidden=�� .+�	� -   @ 	�Name$@ 
chkCozinha��A 	� 	GroupName$A pedidos��$��Cozinha��$��0 0 20 0q�	� -   $@ chkBar��$A pedidos��$	��Bar��B d� 	IsChecked$
B TrueY��� -   C ��Name$C VerticalScroller_cozinha��$��2��$y�Hidden=�$��Disabled=�$��Visible=���. $��10 10 10 10q�.���� -   D ��Name$D grid_pedido_cozinha������ .R�A� E A� Columns$E 4������ .R��� -   $��3 0 3 5q�$��85��$��55������ *cor+���� *
corFonte+: �� ���� *cor+���� *id+.���� .+��� $��Center=����� *	CodigoPrd+�� ���� *	descricao+�� -   $C VerticalScroller_bar��$y�Visible=�$��2��$��Disabled=�$��Visible=���. $��10 10 10 10q�.���� -   $D grid_pedido_bar��$	��Top=����� .R�A� $E 4������ .R��� -   $��3 0 3 5q�$��85��$��55������ *cor+���� *cor+���� *
corFonte+: �� .���� .+��� $��Center=����� *	CodigoPrd+�� ���� *	descricao+�� $��2��$��2��#��[ $��
0,10,20,10q�._�� .+��� $
��Left=�$	��Top=�$��9(�$��5 20 0 0q�.���� .+��� O�� B�� S�� E�� R�� V�� -   F ��Name$F txt_obs��{�. G �� TextWrapping$
G Wrap=�H �� AcceptsReturnH . "I �� VerticalScrollBarVisibility$
I Auto=�$	��492��$
��Left=�$��
34,10,0,90q�
�������$ 1Q��� -   $C VerticalScroller3��$��Disabled=�$��Visible=���. $��28,50,10,10q�.���� -   $D grid3������ .R�A� $E 5������ .R�d� J d�Name$J btObs��-   $��3 3 5 2q����#B68944���#B68944$��95��$��28������ *idObs+���� *	descricao+��   BordaEscura#��[ $��0��$��3��$��2��$��	0 2 20 17q�   $
 Gray��$ .9Q�$ 8Q�._�� $��Center=�$��Center=�.+��� -   $? 	Finalizar��$��0 0 60 0q�#��[ $��Concluir��$	��112��$��30���� -   $? Exluir��$��	10 0 60 0q�#��[ $��Excluir��$	��112��$��30���� -   $? btVoltar��$��10 0 0 0q�#��[ $��Voltar��$	��112��$��30��