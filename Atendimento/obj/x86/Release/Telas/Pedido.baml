   M S B A M L   `   `   `  ���� I  EAtendimentoPDV, Version=1.1.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Artebit.Restaurante.AtendimentoPDV.Controles,Artebit.Restaurante.AtendimentoPDV.Controles  g8clr-namespace:Artebit.Restaurante.AtendimentoPDV.Classes*Artebit.Restaurante.AtendimentoPDV.Classes  Y UArtebitGourmet.TecladoVirtual2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�]clr-namespace:ArtebitGourmet.TecladoVirtual2.Keyboard;assembly=ArtebitGourmet.TecladoVirtual2'ArtebitGourmet.TecladoVirtual2.Keyboard n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7�Zclr-namespace:Telerik.Windows.Controls.GridView;assembly=Telerik.Windows.Controls.GridView!Telerik.Windows.Controls.GridView e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7eHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.ControlsTelerik.Windows.Controls 5    /Artebit.Restaurante.AtendimentoPDV.Telas.Pedido   H	Controles:clr-namespace:Artebit.Restaurante.AtendimentoPDV.Controles  m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation        \	 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 	 
  R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  	 
  Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  	 
  @d2http://schemas.microsoft.com/expression/blend/2008  	 
  Jlocal8clr-namespace:Artebit.Restaurante.AtendimentoPDV.Classes  	 
  kk]clr-namespace:ArtebitGourmet.TecladoVirtual2.Keyboard;assembly=ArtebitGourmet.TecladoVirtual2  	 
  vtelerikGridViewZclr-namespace:Telerik.Windows.Controls.GridView;assembly=Telerik.Windows.Controls.GridView  	 
  ]ControlsHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls  	 
  
������   � 	Resources  D   >Artebit.Restaurante.AtendimentoPDV.Classes.VisibilityConverter   visibilityConverter  LinearGradient_Orange_H �� EndPoint �� 
StartPoint � Color � Offset  Horizontal_Gradient  Vertical_Gradient  MouseOverBrush  
LightBrush �� 
TargetType �� Property �� Value  NormalBrush  NormalBorderBrush �� 
TargetType	 �� 
Background
 �� Bullet ��Name  Stroke_Gradient �� CornerRadius F�Name J� SnapsToDevicePixels �� StrokeEndLineCap �� StrokeStartLineCap �� Triggers P� Property P� Value �� 
TargetName 	 Normal_Background 
 PressedBorderBrush  DisabledBorderBrush  
LeftButton ��Name  RightButton  UpButton  
DownButton��%R
  &	        &	      &	 �     &	 �     &	 =    &	 �    '�� �    &	 �    &	 /    &	 g    &	 �      �� $	 0,1&�$	 0,0&�.��� $ 	#FFF9FF09��$ 0Q�� $ 	#FFF7DE25��$ 0.959Q�� $ 	#FFFF7000��$ 0.033Q�� $ 	#FFFF7200��$ 1Q��� $	 1,0&�$	 0,0&�.��� $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $	 0,1&�$	 0,0&�.��� $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $	 0,1&�$	 0,0&�.��� $
 #FFF��$	 0.0Q�� $
 #AAA��$	 1.0Q��� $	 0,1&�$	 0,0&�.��� $
 #FFF��$	 0.0Q�� $
 #EEE��$	 1.0Q��� " ��.���  �@J�SnapsToDevicePixels . ��  � ��# �  ��  � ��# �  ��  � �� �� " ��.R��� 
	 ���� 
 � $��30��$��30��.+��� $ Border��#���  #���  $��1,1,1,1q�$ 1,1,1,1��F� $ 	CheckMark��
���   �$��5�� .  S������  �?��:gUU�?��	��#@�sVUU#@��K��?�sVUU#@K���#@���  �?	$��9 9 0 0q�}�.  $ Round=�$ Round=�._��� #��z��#��z��#��z����.  P�  �@d�	IsChecked$ falseY�. ��� $ 	CheckMark�� � y�$ 	Collapsed=�P�  �@J�IsMouseOver . . ��� # �  $ Border�� � ��P�  � �� . . ��� # � 	 $ Border�� � ���� # � 
 $ Border�� � ��P�  � {� .  . ��� # �  $ Border�� � ���� # �  $ Border�� � ���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 90����  � ��$ 20����  � �� �� " ��.R�� $��20��$��90��
������ .+��� $ backImg��$��20��$��30��$#��../Img/setaEsquerdaPedido.png�� P�  � {� .  . ��� $ backImg�� � ��$( "../Img/setaEsquerdaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 90����  � ��$ 20����  � �� �� " ��.R�� $��20��$��90��
������ .+��� $ backImg��$"��../Img/setaDireitaPedido.png�� P�  � {� .  . ��� $ backImg�� � ��$' !../Img/setaDireitaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 60����  � ��$ 30����  � �� �� " ��.R�� $��30��$��60��
������ .+��� $ backImg��$��../Img/setaCimaPedido.png��$	��Top=� P�  � {� .  . ��� $ backImg�� � ��$$ ../Img/setaCimaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 60����  � ��$ 30����  � �� �� " ��.R�� $��30��$��60��
������ .+��� $ backImg��$ ��../Img/setaBaixoPedido.png��$��Bottom=� P�  � {� .  . ��� $ backImg�� � ��$% ../Img/setaBaixoPedidoOpaco.png��.���� .+�� .+�=   7Artebit.Restaurante.AtendimentoPDV.Controles.Cabecalho1  -     Name$ 	Cabecalho��   Titulo$ Atendimento��� -    �Name$ 
GridGlobal��$
��1000��$	��650��$��15 0 0 0q� � ColumnDefinitions �� $	��150 ��� $	��420 ��� $	��220 ��� $	��210 � � RowDefinitions �� $	��152 ��� $	��152 ��� $	��152 ��� $��93 ��� $��97 �.+�� $��0��$��2��$��0��$��3��$��0 0 0 0q�    RowStyle3  -Telerik.Windows.Controls.GridView.GridViewRow J� Opacity ��Name �� StringFormat)  #Telerik.Windows.Controls.RadRowItem  Headerstyle9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color    ShadowDepth!   Opacity  Footerstyle9  3Telerik.Windows.Controls.GridView.GridViewFooterRow" ��Name# �� Path$ �� RelativeSource% �� AncestorType��%b	  &	       &	 �    &	 �    �� "  .���  � ��$ 45����  � �� �� "  .R��� $ borda1��
�������$	 0.8Q�._�� $ SelectedRow�� �� $��51 ��� $	��289 ��� $��60 ��� $��60 ��� $��80 �.+��� ���#49181A$��0q�$��0��$	 0.8Q�._��� $	 chk��-   $��Center=�$��Center=����� *nuItem+�� ���#49181A$��1q�$��1��._��� $��5 0 0 0q�$��Center=����� *nome+�� ���#49181A$��1q�$��2��._��� $��Center=�$��Center=����� *und+�� ���#49181A$��1q�$��3��._��� $��Center=�$��Center=����� *qtd+�� ���#49181A$��1q�$��4��._��� $��Center=�$��Center=����� *	precoView+$ {0:c}�� P�  �@ 
IsSelected . . ��� $ borda1�� � ��
 � ����� "  .���  � ��$ 51����  � �� �� "  .R��� $ borda1�����#B68944���#49181A$��0 0 0 3q�   $ Black��$  5Q�$	! 0.9Q�._��  �� $	��340 ��� $��60 ��� $��60 ��� $��80 �.+��� ���#49181A$��1q�$��0��._��� $��35 0 0 0q�
�������$��Center=�$��15(�$��Produto���� ���#49181A$��1q�$��1��._��� $��Center=�
�������$��Center=�$��15(�$	��Und���� ���#49181A$��1q�$��2��._��� $��Center=�
�������$��Center=�$��14(�$	��Qtd���� ���#49181A$��1q�$��3��._��� $��Center=�$��Center=�
�������$��14(�$��Preço (R$)���� "  .���  � ��$ 53����  � �� �� "  .R��� $ borda1�����#6B4523���#49181A$��0 2 0 0q�   $ Black��$  5Q�$	! 0.9Q�._��  �� $��51 ��� $��28 ��� $	��201 ��� $	��120 ��� $��59 ��� $��2 ��� $��80 �.+��� ���#49181A$��0q�$��0��$	 0.8Q�._��� $ 	chk_total��-   $��Center=�$��Center=��� $��0q�$��1��._��� $" nupedido��$��5 0 0 0q�
�������$��Center=�$��Right=�$��15(����� $# Pedidos�$ �� $% telerik:RadGridViewL��� $��0q�$��2��._��� 
�������$��Center=�$
��Left=�$��15(�$��Pedidos���� $��0q�$��3��._��� $��Right=�
�������$��Center=�$��15(�$��Total de produtos:���� $��0q�$��4��._��� $
" cont��$��Center=�
�������$��Center=�$��15(�$�� ���� ���#49181A$��2q�$��5���� $��0q�$��6��._��� $" totpreco��$��Center=�
�������$��Center=�$��15(�$�� ��.+�/  )Telerik.Windows.Controls.RadBusyIndicator  -   &  Name$& busyIndicator��.��*	  $Telerik.Windows.Controls.RadGridView	  -   ' 	 Name$' 	gridDados��;
  5Telerik.Windows.Controls.GridView.GridViewDataControl( 
  SelectionMode$( Single=�$	��570��
������ ) 
  CanUserFreezeColumns) .  * 
  RowIndicatorVisibility$* 	Collapsed=�+ 
  AutoGenerateColumns+ .  , 
  
IsReadOnly, . - 
  ShowGroupPanel- .  . 
  SelectionUnit$. FullRow=�$
��Left=�$��0q�
������ / 
  RowStyle#/ [ 0 
  HeaderRowStyle#0 [ 1 
  FooterRowStyle#1 [ 2 
  ShowColumnFooters2 . $��Visible=�$��Disabled=�3 
  Columns3 3  -Telerik.Windows.Controls.GridViewSelectColumn  1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase4   Header$4 Cód.��-  'Telerik.Windows.Controls.GridViewColumn5   IsGroupable5 .  6   	IsVisible6 .  7   IsFilterable7 .  8   DataMemberBinding8 �� *nuItem+  $4 Descrição��5 . 7 . 8 �� *nome+  $4 Preço��5 . 7 . 9   DataFormatString$9 {0:c}��8 �� *preco+�� $��2��$��2��$��0��$��0 3 0 2q�  
BordaClara#��[ ._�� .+��� $
��Left=�$	��Top=�$��10(�$��5 15 0 0q�.���� .+��� C�� A�� T�� E�� G�� O�� R�� I�� A�� -   : ��Name$: VerticalScroller1��$��Disabled=�$��Hidden=���. $��	20,3,40,2q�.���� -   ; ��Name$; grid1��$	��Top=����� .R�A� < A� Columns$< 3������ .R��� -	   $��2 2 2 2q�$��Segoe UI)�$��14(�$	��118��$��30������ *cor+���� *
corFonte+���� *cor+= � DataContext= �� y��� *ativo+> �� 	Converter#> [  ���� *	descricao+���� *id+� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -
   ? ��Name$? ubtCat1��$��1��#��[ $	��Top=�$��2 7 3 0q��� -   $? dbtCat1��$��1��#��[ $��Bottom=�$��2 0 3 0q��� $��2��$��2��$��1��$��0 3 0 2q�#��[ ._�� .+��� $
��Left=�$	��Top=�$��10(�$��5 7 0 0q�.���� .+��� G�� U�� A�� R�� N�� $��2 0 0 0q�I�� Ç�� Ã�� Od� -   @ d�Name$@ btAdd��$��5 3 50 0q�$	��Top=�$��Right=�$��	Adicional��$	 0.8Q�$	��110��$��23���� -   $: VerticalScroller2��$��Disabled=�$��Hidden=���. $��22 30 40 10q�.���� -   $; grid2��$	��Top=����� .R�A� $< 3������ .R��� -   $��2 2 2 2q�$��Segoe UI)�$��14(�$	��118��$��30������ *cor+���� *cor+���� *
corFonte+= �� y��� *ativo+#> [  ���� *	descricao+� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $? ubtCat2��$��1��#��[ $	��Top=�$��2 7 3 0q��� -   $? dbtCat2��$��1��#��[ $��Bottom=�$��2 0 3 7q��� $��2��$��2��$��2��$��0 3 0 2q�#��[ ._�� .+��� $
��Left=�$	��Top=�$��9(�$��5 0 0 0q�.���� .+��� O�� B�� S�� E�� R�� V�� A�� Ç�� Õ�� E�� S�� -   A ��Name$A txt_obs��B �� TextWrapping$
B Wrap=�C �� AcceptsReturnC . "D �� VerticalScrollBarVisibility$
D Auto=�$	��342��{�. $
��Left=�$��
28,115,0,5q�
�������$ 1Q�A  ;ArtebitGourmet.TecladoVirtual2.Keyboard.TouchScreenKeyboardE   TouchScreenKeyboardE . �� -   $: VerticalScroller3��$��Disabled=�$��Hidden=���. $��
20 3 40 35q�.���� -   $; grid3������ .R�A� $< 3������ .R�d� $@ btObs��-   $��3 4 5 4q�$	��110��$��30������ *idObs+���� *	descricao+= �� � $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $? ubtObs��$��1��#��[ $	��Top=�$��2 7 3 0q��� -   $? dbtObs��$��1��#��[ $��Bottom=�$��2 0 3 7q��� $��1��$��3��$��3��$��10 2 0 2q�#��[ ._�� .+��� $
��Left=�$	��Top=�$��10(�$��28 0 0 0q�.���� .+��� C�� O�� M�� $��	2.5 0 0 0q�I�� D�� A�� -   $: HorizontalScroller1��$��Hidden=�$��Disabled=���. $��	45 5 35 5q�.���� -   $; grid_cozinha������ .R�A� $< 6������ .R��� -   $��2 2 2 2q�$��Segoe UI)�$��14(�$	��120��$��35������ *cor+���� *
corFonte+���� *cor+= �� y��� *ativo+#> [  ���� *	descricao+� $
��left=� �� �� .+��� $��1��$��0�����#E8E8E8$��1q��� -   $? 	lbtGrupo1��$��0��#��[ $��Center=�$��5 7 4 0q�� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $? 	rbtGrupo1��$��1��#��[ $��Center=�$��5 7 7 0q��� $��1��$��4��$��3��$��10 2 0 2q�#��[ ._�� .+��� $
��Left=�$	��Top=�$��10(�$��28 0 0 0q�.���� .+��� B�� E�� B�� $��	1.5 0 0 0q�I�� D�� A�� -   $: HorizontalScroller2��$��Hidden=�$��Disabled=���. $��	45 5 35 5q�.���� -   $; grid_bar������ .R�A� $< 6������ .R��� -   $��2 2 2 2q�$��Segoe UI)�$��14(�$	��120��$��35������ *cor+���� *
corFonte+���� *cor+���� *
idItemCard+= �� y��� *ativo+#> [  ���� *	descricao+� $
��left=� �� �� .+��� $��1��$��0�����#E8E8E8$��1q��� -    $? 	lbtGrupo2��$��0��#��[ $��Center=�$��5 7 4 0q�� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -!   $? 	rbtGrupo2��$��1��#��[ $��Center=�$��5 7 7 0q���   BordaEscura#��[ $��0��$��3��$��2��$��0 2 0 2q�._��� $��Center=�.+��� -"   F ��Name$F 	Finalizar��$��0 0 0 0q�$��23(�$��Concluir��$	��140��$��35���� -#   $F Exluir��$��0 10 0 0q�$��23(�$��Excluir��$	��140��$��35���� -$   $F 
Quantidade��$��0 10 0 0q�$��23(�$��
Quantidade��$	��140��$��35���� -%   $F btVoltar��$��0 10 0 0q�$��23(�$��Voltar��$	��140��$��35��