   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa8    2Artebit.Restaurante.Caixa.Caixas.Janelas.Adicionar   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    $	��500��$	��890��$��Adicionar Item��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�
������� � 	Resources (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter*  $Telerik.Windows.Controls.RadGridView* 
 $Telerik.Windows.Controls.RadComboBox   
LeftButton �� 
TargetType ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName  RightButton  UpButton  
DownButton��%2  '        '  /     '  [     &	  �     &	 �    &	 �    &	 �    �� "  .���  �@ Theme$ Windows7 �� "  .���  �@ Theme$ Metro �� "  .���  �@ Theme$ Summer �� " ��.���  � |� .  ��  � �� .  ��  � ��$ 80����  � ��$ 10����  � �� �� " ��.R�� .+��� $ backImg��$#��../Img/setaEsquerdaPedido.png�� P� 	 � {�
 .  . ��� $ backImg�� � ��$( "../Img/setaEsquerdaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 80����  � ��$ 10����  � �� �� " ��.R�� .+��� $ backImg��$"��../Img/setaDireitaPedido.png�� P� 	 � {�
 .  . ��� $ backImg�� � ��$' !../Img/setaDireitaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 30����  � ��$ 15����  � �� �� " ��.R�� .+��� $ backImg��$��../Img/setaCimaPedido.png�� P� 	 � {�
 .  . ��� $ backImg�� � ��$$ ../Img/setaCimaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 30����  � ��$ 15����  � �� �� " ��.R�� .+��� $ backImg��$ ��../Img/setaBaixoPedido.png�� P� 	 � {�
 .  . ��� $ backImg�� � ��$% ../Img/setaBaixoPedidoOpaco.png��.��� -    �Name$ 
LayoutRoot��   RowStyle3  -Telerik.Windows.Controls.GridView.GridViewRow ��Name J� Opacity � ColumnDefinitions)	 	 #Telerik.Windows.Controls.RadRowItem  Headerstyle9
  3Telerik.Windows.Controls.GridView.GridViewHeaderRow J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   ShadowDepth   Opacity  Footerstyle9  3Telerik.Windows.Controls.GridView.GridViewFooterRow ��Name �� Path �� RelativeSource �� AncestorType��%�  &	       &	 <    &	 k    �� "  .���  � ��$ 30����  � �� �� "  .R��� $ borda1��
�������$	 0.8Q�._�� $ SelectedRow�� �� $	��310 �.+��� ���#49181A$��1q�$��1��._��� $��5 0 0 0q�$��Center=����� *nome+ P� 	 �@	 
IsSelected
 . . ��� $ borda1�� � ��
 � ����� " 
 .���  � ��$ 20����  � �� �� " 
 .R��� $ borda1�����#B68944���#49181A$��0 0 0 0q�   $ Black��$ 5Q�$	 0.9Q�._��  �� $	��340 �.+��� ���#49181A$��1q�$��0��._��� $��	15 -3 0 0q�
�������$��Center=�$��14(�$��Produto���� "  .���  � ��$ 53����  � �� �� "  .R��� $ borda1�����#6B4523���#49181A$��0 2 0 0q�   $ Black��$ 5Q�$	 0.9Q�._��  �� $��31 ��� $��31 ��� $	��218 ��� $	��120 ��� $��59 ��� $��2 ��� $��80 �.+��� $��0q�$��1��._��� $
 lulu��$��5 0 0 0q�
�������$��Center=�$��Right=�$��13(����� $ Pedidos� �� $ telerik:RadGridViewL��� $��0q�$��2��._��� $
 foda��
�������$��Center=�$
��Left=�$��13(�$��Pedidos���� $��0q�$��3��._��� $��Right=�
�������$��Center=�$��13(�$��Total de produtos:���� $��0q�$��4��._��� $��Center=�
�������$��Center=�$��13(�$��41���� ���#49181A$��2q�$��5���� $��0q�$��6��._��� $��Center=�
�������$��Center=�$��13(�$��145,00��.+��� $��Bottom=�$��1�����#FEFEFE$	��880��$��60��._�� .+�  -   $��btnOK��$��OK��$��0 0 100 -10q�$��Right=�$��80��$��25��  BotaoMenor2#��[   -   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[ �� $��0,4,247,391q�  
BordaClara#��[ $��Right=�$	��315��._�� .+��� $��COZINHA��$��27��$
��Left=�$	��Top=�$��13(�$��	22,-4,0,0q��� -    ��Name$ HorizontalScroller1��$��Hidden=�$��Disabled=���. $��	13,19,0,6q�$
��Left=�$	��285��.���� -    ��Name$ grid_cozinha������ .R�A� -   ���� .R��� -   $��6 4 4 10q�#��[ $��83��$��30������ *id+���� *nome+� $
��left=� �� �� .+��� $��1��$��0�����#E8E8E8$��1q��� -    ��Name$ LeftButton1��$��0��#��[  $��Center=�$��2 7 1 2q�� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -	   $ RightButton1��$��1��#��[ $��Center=�$��2 7 1 2q��� $��0,3,563,391q�#��[ ._�� .+��� $	��BAR��$��27��$
��Left=�$	��Top=�$��13(�$��	22,-4,0,0q��� -
   $ HorizontalScroller2��$��Hidden=�$��Disabled=���. $��	13,21,0,6q�$
��Left=�$	��285��.���� -   $ grid_bar������ .R�A� -   ���� .R��� -   $��6 4 4 10q�#��[ $��83��$��30������ *id+���� *nome+� $
��left=� �� �� .+��� $��1��$��0�����#E8E8E8$��1q��� -   $ LeftButton2��$��0��#��[  $��Center=�$��2 7 1 2q�� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $ RightButton2��$��1��#��[ $��Center=�$��2 7 1 2q��� $��0,81,247,314q�#��[ ._�� .+��� $��CATEGORIA 1��$��27��$
��Left=�$	��Top=�$��13(�$��	22,-4,0,0q��� -   $ HorizontalScroller3��$��Hidden=�$��Disabled=���. $��	15,19,0,6q�$
��Left=�$	��601��.���� -   $ grid1������ .R�A� ���� .R��� -   $��3 0 3 5q�#��[ $��95��$��30������ *id+���� *nome+� $
��left=� �� �� .+��� $��1��$��0�����#E8E8E8$��1q��� -   $ LeftButton3��$��0��#��[  $��Center=�$��2 7 1 2q�� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $ RightButton3��$��1��#��[ $��Center=�$��2 7 1 2q��� $��0,158,0,242q�#��[ $
��Left=�$	��631��._�� .+��� $��OPÇÃO/GUARNIÇÃO��$��27��$
��Left=�$	��Top=�$��13(�$��	22,-4,0,0q��� -   $ HorizontalScroller4��$��Hidden=�$��Disabled=���. $��	15,20,0,6q�$
��Left=�$	��600��.���� -   $ grid2������ .R�A� ���� .R��� -   $��3 0 3 5q�#��[ $��95��$��30������ *id+���� *nome+� $
��left=� �� �� .+��� $��1��$��0�����#E8E8E8$��1q��� -   $ LeftButton4��$��0��#��[  $��Center=�$��2 7 1 2q�� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $ RightButton4��$��1��#��[ $��Center=�$��2 7 1 2q��� $��0,230,0,168q�#��[ $
��Left=�$	��629��._�� .+��� $��
ADICIONAIS��$��27��$
��Left=�$	��Top=�$��13(�$��	22,-4,0,0q��� -   $ HorizontalScroller5��$��Hidden=�$��Disabled=���. $��
15,19,14,6q�.���� -   $ grid5������ .R�A� ���� .R��� -   $��3 0 3 5q�#��[ $��95��$��30������ *id+���� *nome+� $
��left=� �� �� .+��� $��1��$��0�����#E8E8E8$��1q��� -   $ LeftButton5��$��0��#��[  $��Center=�$��2 7 1 2q�� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -   $ RightButton5��$��1��#��[ $��Center=�$��2 7 1 2q��� $��2,309,251,74q�#��[ ._�� .+��� -   $��label5��$��OBSERVAÇÕES��$��27��$
��Left=�$	��Top=�$��13(�$��5 3 0 0q��� -     ��Name$ txt_obs�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�$	��200��$
��Left=�$��8,29,0,2q�
�������$ 1Q��� -!   $ VerticalScroller6��$��Disabled=�$��Hidden=���. $��220,29,26,2q�.���� -"   $ grid6������ .R�A�  A� Columns$ 3������ .R�d�   d�Name$  btObs��-#   $��3 2 3 17q�$��85��$��25������ *idObs+���� *	descricao+� $��Right=� �� �� .+��� $��0��$��0�����#E8E8E8$��1q��� -$   $ 	UpButton6��$��1��#��[ $	��Top=�$��2 7 1 2q��� -%   $ DownButton6��$��1��#��[ $��Bottom=�$��2 7 1 2q�  -&   !  Name$! 	gridDados��;  5Telerik.Windows.Controls.GridView.GridViewDataControl"   SelectionMode$" Single=�
������ #   CanUserFreezeColumns# .  $   RowIndicatorVisibility$$ 	Collapsed=�%   AutoGenerateColumns% .  &   
IsReadOnly& . '   ShowGroupPanel' .  (   SelectionUnit$( FullRow=�$
��Left=�$��0q�
������ )   RowStyle#) [ *   HeaderRowStyle#* [ +   FooterRowStyle#+ [ ,   ShowColumnFooters, .  $��Visible=�$��Disabled=�$��	635,5,0,0q�$	��Top=�$	��386��$	��243��-   Columns- 1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase.   Header$. Descrição��-  'Telerik.Windows.Controls.GridViewColumn/   IsGroupable/ . 0   IsFilterable0 . 1   DataMemberBinding1 �� *nome+