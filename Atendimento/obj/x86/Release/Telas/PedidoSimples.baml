   M S B A M L   `   `   `  ���� Y  UArtebitGourmet.TecladoVirtual2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�]clr-namespace:ArtebitGourmet.TecladoVirtual2.Keyboard;assembly=ArtebitGourmet.TecladoVirtual2'ArtebitGourmet.TecladoVirtual2.Keyboard  n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7�Zclr-namespace:Telerik.Windows.Controls.GridView;assembly=Telerik.Windows.Controls.GridView!Telerik.Windows.Controls.GridView e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7eHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.ControlsTelerik.Windows.Controls  AtendimentoPDV<   6Artebit.Restaurante.AtendimentoPDV.Telas.PedidoSimples   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation        \	 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 	 
  R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  	 
  Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  	 
  @d2http://schemas.microsoft.com/expression/blend/2008  	 
  kk]clr-namespace:ArtebitGourmet.TecladoVirtual2.Keyboard;assembly=ArtebitGourmet.TecladoVirtual2  	 
  vtelerikGridViewZclr-namespace:Telerik.Windows.Controls.GridView;assembly=Telerik.Windows.Controls.GridView  	 
  ]ControlsHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls  	 
  
������   � 	Resources     LinearGradient_Orange_H �� EndPoint �� 
StartPoint � Color � Offset  Horizontal_Gradient  Vertical_Gradient  MouseOverBrush  
LightBrush �� 
TargetType �� Property �� Value  NormalBrush  NormalBorderBrush �� 
TargetType	 �� 
Background
 �� Bullet ��Name  Stroke_Gradient �� CornerRadius F�Name J� SnapsToDevicePixels �� StrokeEndLineCap �� StrokeStartLineCap �� Triggers P� Property P� Value �� 
TargetName  Normal_Background 	 PressedBorderBrush 
 DisabledBorderBrush  
LeftButton ��Name  RightButton  UpButton  
DownButton��%�	  &	        &	 �     &	 �     &	 8    &	 �    '�� �    &	 �    &	 �    &	     &	 +    �� $	 0,1&�$	 0,0&�.��� $ 	#FFF9FF09��$ 0Q�� $ 	#FFF7DE25��$ 0.959Q�� $ 	#FFFF7000��$ 0.033Q�� $ 	#FFFF7200��$ 1Q��� $	 1,0&�$	 0,0&�.��� $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $	 0,1&�$	 0,0&�.��� $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $	 0,1&�$	 0,0&�.��� $
 #FFF��$	 0.0Q�� $
 #AAA��$	 1.0Q��� $	 0,1&�$	 0,0&�.��� $
 #FFF��$	 0.0Q�� $
 #EEE��$	 1.0Q��� " ��.���  �@J�SnapsToDevicePixels . ��  � ��# �  ��  � ��# �  ��  � �� �� " ��.R��� 
	 ���� 
 � $��30��$��30��.+��� $ Border��#���  #���  $��1,1,1,1q�$ 1,1,1,1��F� $ 	CheckMark��
���   �$��5�� .  S������  �?��:gUU�?��	��#@�sVUU#@��K��?�sVUU#@K���#@���  �?	$��9 9 0 0q�}�.  $ Round=�$ Round=�._��� #��z��#��z��#��z����.  P�  �@d�	IsChecked$ falseY�. ��� $ 	CheckMark�� � y�$ 	Collapsed=�P�  �@J�IsMouseOver . . ��� # �  $ Border�� � ��P�  � �� . . ��� # �  $ Border�� � ���� # � 	 $ Border�� � ��P�  � {� .  . ��� # �  $ Border�� � ���� # � 
 $ Border�� � ���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 30����  � ��$ 30����  � �� �� " ��.R�� .+��� $ backImg��$#��../Img/setaEsquerdaPedido.png�� P�  � {� .  . ��� $ backImg�� � ��$( "../Img/setaEsquerdaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 30����  � ��$ 30����  � �� �� " ��.R�� .+��� $ backImg��$"��../Img/setaDireitaPedido.png�� P�  � {� .  . ��� $ backImg�� � ��$' !../Img/setaDireitaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 30����  � ��$ 30����  � �� �� " ��.R�� .+��� $ backImg��$��../Img/setaCimaPedido.png�� P�  � {� .  . ��� $ backImg�� � ��$$ ../Img/setaCimaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 30����  � ��$ 30����  � �� �� " ��.R�� .+��� $ backImg��$ ��../Img/setaBaixoPedido.png�� P�  � {� .  . ��� $ backImg�� � ��$% ../Img/setaBaixoPedidoOpaco.png��.��� -    �Name$ 
GridGlobal��$	��990��$	��650�� � ColumnDefinitions �� $	��150 ��� $	��440 ��� $	��200 ��� $	��200 � � RowDefinitions �� $	��247 ��� $	��247 ��� $	��105 ��� $��47 �.+�� $��0��$��2��$��0��$��3��$��0 0 20 0q�    RowStyle3  -Telerik.Windows.Controls.GridView.GridViewRow J� Opacity ��Name �� StringFormat)  #Telerik.Windows.Controls.RadRowItem  Headerstyle9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   ShadowDepth   Opacity  Footerstyle9  3Telerik.Windows.Controls.GridView.GridViewFooterRow  ��Name! �� Path" �� RelativeSource# �� AncestorType��%^	  &	       &	 �    &	 �    �� "  .���  � ��$ 45����  � �� �� "  .R��� $ borda1��
�������$	 0.8Q�._�� $ SelectedRow�� �� $��51 ��� $	��289 ��� $��60 ��� $��60 ��� $��80 �.+��� ���#49181A$��0q�$��0��$	 0.8Q�._��� $	 chk��-   $��Center=�$��Center=����� *nuItem+�� ���#49181A$��1q�$��1��._��� $��5 0 0 0q�$��Center=����� *nome+�� ���#49181A$��1q�$��2��._��� $��Center=�$��Center=����� *und+�� ���#49181A$��1q�$��3��._��� $��Center=�$��Center=����� *qtd+�� ���#49181A$��1q�$��4��._��� $��Center=�$��Center=����� *preco+$ {0:c}�� P�  �@ 
IsSelected . . ��� $ borda1�� � ��
 � ����� "  .���  � ��$ 51����  � �� �� "  .R��� $ borda1�����#B68944���#49181A$��0 0 0 3q�   $ Black��$ 5Q�$	 0.9Q�._��  �� $	��340 ��� $��60 ��� $��60 ��� $��80 �.+��� ���#49181A$��1q�$��0��._��� $��35 0 0 0q�
�������$��Center=�$��14(�$��Produto���� ���#49181A$��1q�$��1��._��� $��Center=�
�������$��Center=�$��14(�$	��Und���� ���#49181A$��1q�$��2��._��� $��Center=�
�������$��Center=�$��14(�$	��Qtd���� ���#49181A$��1q�$��3��._��� $��Center=�$��Center=�
�������$��14(�$��Preço (R$)���� "  .���  � ��$ 53����  � �� �� "  .R��� $ borda1�����#6B4523���#49181A$��0 2 0 0q�   $ Black��$ 5Q�$	 0.9Q�._��  �� $��51 ��� $��28 ��� $	��201 ��� $	��120 ��� $��59 ��� $��2 ��� $��80 �.+��� ���#49181A$��0q�$��0��$	 0.8Q�._��� $ 	chk_total��-   $��Center=�$��Center=��� $��0q�$��1��._��� $  nupedido��$��5 0 0 0q�
�������$��Center=�$��Right=�$��13(����� $! Pedidos�" �� $# telerik:RadGridViewL��� $��0q�$��2��._��� 
�������$��Center=�$
��Left=�$��13(�$��Pedidos���� $��0q�$��3��._��� $��Right=�
�������$��Center=�$��13(�$��Total de produtos:���� $��0q�$��4��._��� $
  cont��$��Center=�
�������$��Center=�$��13(�$�� ���� ���#49181A$��2q�$��5���� $��0q�$��6��._��� $  totpreco��$��Center=�
�������$��Center=�$��13(�$�� ��.+�/  )Telerik.Windows.Controls.RadBusyIndicator  -   $  Name$$ busyIndicator��.��*  $Telerik.Windows.Controls.RadGridView  -   %  Name$% 	gridDados��;  5Telerik.Windows.Controls.GridView.GridViewDataControl&   SelectionMode$& Single=�$	��570��
������ '   CanUserFreezeColumns' .  (   RowIndicatorVisibility$( 	Collapsed=�)   AutoGenerateColumns) .  *   
IsReadOnly* . +   ShowGroupPanel+ .  ,   SelectionUnit$, FullRow=�$
��Left=�$��0q�
������ -   RowStyle#- [ .   HeaderRowStyle#. [ /   FooterRowStyle#/ [ 0   ShowColumnFooters0 . $��Visible=�$��Disabled=�1   Columns1 3	  -Telerik.Windows.Controls.GridViewSelectColumn	  1
  +Telerik.Windows.Controls.GridViewDataColumn
  6  0Telerik.Windows.Controls.GridViewBoundColumnBase2   Header$2 Cód.��-  'Telerik.Windows.Controls.GridViewColumn3   IsGroupable3 .  4   	IsVisible4 .  5   IsFilterable5 .  6   DataMemberBinding6 �� *nuItem+
  $2 Descrição��3 . 5 . 6 �� *nome+
  $2 Preço��3 . 5 . 7   DataFormatString$7 {0:c}��6 �� *preco+�� $��2��$��2��$��0��$��0 3 0 2q�  
BordaClara#��[ ._�� .+��� -   $��label3��$	��BAR��$��27��$
��Left=�$	��Top=�$��13(�$��5 3 0 0q��� -   8 ��Name$8 VerticalScroller_bar��$��Disabled=�$��Hidden=���. $��10 30 50 10q�.���� -   9 ��Name$9 grid_bar������ .R�A� : A� Columns$: 3������ .R��� -	   $��3 0 3 5q�$	��105��$��30������ *cor+���� *cor+���� *id+���� *nome+� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -
   ; ��Name$; UpButton_bar��$��1��#��[ $	��Top=�$��5 7 7 0q��� -   $; DownButton_bar��$��1��#��[ $��Bottom=�$��5 0 7 7q��� $��2��$��2��$��1��$��0 3 0 2q�#��[ ._�� .+��� -   $��label4��$��COZINHA��$��27��$
��Left=�$	��Top=�$��13(�$��5 3 0 0q��� -   $8 VerticalScroller_cozinha��$��Disabled=�$��Hidden=���. $��10 30 50 10q�.���� -   $9 grid_cozinha������ .R�A� $: 3������ .R��� -   $��3 0 3 5q�$	��105��$��28������ *cor+���� *cor+���� *id+���� *nome+� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $; UpButton_cozinha��$��1��#��[ $	��Top=�$��5 7 7 0q��� -   $; DownButton_cozinha��$��1��#��[ $��Bottom=�$��5 0 7 7q��� $��2��$��2��$��2��$��2��$��0 3 0 2q�#��[ ._�� .+��� $
��Left=�$	��Top=�$��9(�$��5 0 0 0q�.���� .+��� O�� B�� S�� E�� R�� V�� A�� Ç�� Õ�� E�� S�� -   < ��Name$< txt_obs��= �� TextWrapping$
= Wrap=�> �� AcceptsReturn> . {�. "? �� VerticalScrollBarVisibility$
? Auto=�$	��322��$
��Left=�$��28,104,0,15q�
�������$ 1Q�A   ;ArtebitGourmet.TecladoVirtual2.Keyboard.TouchScreenKeyboard@   TouchScreenKeyboard@ . �� -   $8 VerticalScroller3��$��Disabled=�$��Hidden=���. $��28,10,50,48q�.���� -   $9 grid3������ .R�A� $: 3������ .R�d� A d�Name$A btObs��-   $��3 8 5 7q�$��95��$��28������ *idObs+���� *	descricao+� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $; 	UpButton3��$��1��#��[ $	��Top=�$��5 7 7 0q��� -   $; DownButton3��$��1��#��[ $��Bottom=�$��5 0 7 7q���   BordaEscura#��[ $��0��$��3��$��2��$��0 2 20 2q�   $
 Gray��$ .9Q�$ 8Q�._�� $��Center=�$��Center=�.+��� -   B ��Name$B 	Finalizar��$��0 0 60 0q�$��Concluir��$	��112��$��30���� -   $B Exluir��$��	10 0 60 0q�$��Excluir��$	��112��$��30���� -   $B btVoltar��$��GoToPage��$��10 0 0 0q�$��Voltar��$	��112��$��30��