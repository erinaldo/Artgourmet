   M S B A M L   `   `   `  ���� I  EAtendimentoPDV, Version=1.1.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Artebit.Restaurante.AtendimentoPDV.Controles,Artebit.Restaurante.AtendimentoPDV.Controles  n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7�Zclr-namespace:Telerik.Windows.Controls.GridView;assembly=Telerik.Windows.Controls.GridView!Telerik.Windows.Controls.GridView 4    .Artebit.Restaurante.AtendimentoPDV.Telas.Conta   H	Controles:clr-namespace:Artebit.Restaurante.AtendimentoPDV.Controles  m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation        \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R	 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W
 SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation  	 
 R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   	 
 @d2http://schemas.microsoft.com/expression/blend/2008   	 
 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	 
 vtelerikGridViewZclr-namespace:Telerik.Windows.Controls.GridView;assembly=Telerik.Windows.Controls.GridView   	 
 
������ .���� .+�� .+�=   7Artebit.Restaurante.AtendimentoPDV.Controles.Cabecalho1  -      Name$  	Cabecalho��� -    �Name$ 
GridGlobal��$	��990��$	��650��$��15 0 0 0q� � ColumnDefinitions �� $	��840 ��� $	��150 � � RowDefinitions �� $	��240 ��� $	��410 �.+�� $��0��$��0��$��2��$��	0 0 20 10q� � 	Resources    RowStyle �� 
TargetType3  -Telerik.Windows.Controls.GridView.GridViewRow �� Property �� Value �� 
TargetType	 ��Name
 J� Opacity �� Triggers)  #Telerik.Windows.Controls.RadRowItem P� Property P� Value �� 
TargetName  Headerstyle9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow J� Effect3 
 -System.Windows.Media.Effects.DropShadowEffect   Color   ShadowDepth   Opacity  Footerstyle9  3Telerik.Windows.Controls.GridView.GridViewFooterRow ��Name ��Name��%S  &	        &	 �    &	 q	    �� "  .���  � ��$ 45����  � �� �� "  .R��� $	 borda1��
�������$	
 0.8Q�._�� $ SelectedRow�� �� $	��230 ��� $��70 ��� $��70 ��� $��40 ��� $��40 ��� $��80 ��� $��80 ��� $��80 ��� $	��100 �.+��� ���#49181A$��1q�$��0��._��� $��5 0 0 0q�$��Center=����� *nome+�� ���#49181A$��1q�$��1��._��� $��Center=�$��Center=����� *status+�� ���#49181A$��1q�$��2��._��� $��Center=�$��Center=����� *horarioFormat+�� ���#49181A$��1q�$��3��._��� $��Center=�$��Center=����� *und+�� ���#49181A$��1q�$��4��._��� $��Center=�$��Center=����� *	qtdFormat+�� ���#49181A$��1q�$��5��._��� $��Center=�$��Center=����� *precoFormat+�� ���#49181A$��1q�$��6��._��� $��Center=�$��Center=����� *descontoFormat+�� ���#49181A$��1q�$��7��._��� $��Center=�$��Center=����� *totalFormat+�� ���#49181A$��1q�$��8��._��� $��Center=�$��Center=����� *
vendedor+ P�  �@ 
IsSelected . . ��� $ borda1�� � ��
 � ����� "  .���  � ��$ 51����  � �� �� "  .R��� $	 borda1�����#B68944���#49181A$��0 0 0 3q�   $ Black��$ 5Q�$	 0.9Q�._��  �� $	��230 ��� $��70 ��� $��70 ��� $��40 ��� $��40 ��� $��80 ��� $��80 ��� $��80 ��� $	��100 �.+��� ���#49181A$��1q�$��0��._��� $��35 0 0 0q�
�������$��Center=�$��14(�$��Produto���� ���#49181A$��1q�$��1��._��� $��Center=�
�������$��Center=�$��14(�$��Status���� ���#49181A$��1q�$��2��._��� $��Center=�
�������$��Center=�$��14(�$��Horário���� ���#49181A$��1q�$��3��._��� $��Center=�$��Center=�
�������$��14(�$	��Und���� ���#49181A$��1q�$��4��._��� $��Center=�$��Center=�
�������$��14(�$	��Qtd���� ���#49181A$��1q�$��5��._��� $��Center=�$��Center=�
�������$��14(�$��Preço���� ���#49181A$��1q�$��6��._��� $��Center=�$��Center=�
�������$��14(�$��Desconto���� ���#49181A$��1q�$��7��._��� $��Center=�$��Center=�
�������$��14(�$��Subtotal���� ���#49181A$��1q�$��8��._��� $��Center=�$��Center=�
�������$��14(�$��Vendedor���� "  .���  � ��$ 53����  � �� �� "  .R��� $	 borda1�����#6B4523���#49181A$��0 2 0 0q�   $ Black��$ 5Q�$	 0.9Q�._��  �� $	��120 ��� $��48 ��� $��55 ��� $	��186 ��� $��40 ��� $��2 ��� $��78 ��� $��2 ��� $��78 ��� $��2 ��� $��78 �.+��� $��0q�$��0��$	
 0.8Q�._��� $ MarcarTodos��-   $��Marcar��$	��110��$��30���� $��0q�$��1��._��� $ nupedido��$��5 0 0 0q�
�������$��Center=�$��Right=�$��13(�$�� ���� $��0q�$��2��._��� 
�������$��Center=�$
��Left=�$��13(�$��Pedidos���� $��0q�$��3��._��� $��Right=�
�������$��Center=�$��13(�$��Total de produtos:���� $��0q�$��4��._��� $
 cont��$��Center=�
�������$��Center=�$��13(�$�� ���� ���#49181A$��2q�$��5���� $��0q�$��6��._��� $ totpreco��$��Center=�
�������$��Center=�$��13(�$�� ���� ���#49181A$��2q�$��7���� $��0q�$��8��._��� $ totdesconto��$��Center=�
�������$��Center=�$��13(�$�� ���� ���#49181A$��2q�$��9���� $��0q�$��10��._��� $ subtotal��$��Center=�
�������$��Center=�$��13(�$�� ��.+�*  $Telerik.Windows.Controls.RadGridView  -     Name$ 	gridDados��;  5Telerik.Windows.Controls.GridView.GridViewDataControl   SelectionMode$ Multiple=�$	��820��
������    CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�   AutoGenerateColumns .     
IsReadOnly .    ShowGroupPanel .     SelectionUnit$ FullRow=�$
��Left=�$��0q�
������    	RowHeight$ 45Q�   RowStyle# [     HeaderRowStyle# [     FooterRowStyle#  [ !   ShowColumnFooters! . $��Visible=�$��Disabled=�"   Columns" 1	  +Telerik.Windows.Controls.GridViewDataColumn	  6
  0Telerik.Windows.Controls.GridViewBoundColumnBase# 
  Header$# Descrição��-  'Telerik.Windows.Controls.GridViewColumn$   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	$ 230 %   IsGroupable% . &   IsFilterable& .  ' 
  DataMemberBinding' �� *nome+	  $# Status��$$ 70 % . & .  ' �� *status+	  $# Horario��$$ 70 % . & .  ' �� *horarioFormat+	  $	# Und��$$ 40 % . & .  ' �� *und+	  $	# Qtd��$$ 40 % . & .  ' �� *	qtdFormat+	  $# Preco��$$ 80 % . & .  ' �� *precoFormat+	  $# Desconto��$$ 80 % . & .  ' �� *descontoFormat+	  $# Total��$$ 80 % . & .  ' �� *totalFormat+	  $# Vendedor��$	$ 100 % . & .  ' �� *
vendedor+��   BordaEscura#��[ $��1��$��1��$��2��$	��150��$	��410��$
��Left=�$	��Top=�$��0 0 0 10q�$	
 0.8Q�   $
 Gray��$ .9Q�$ 8Q�._��� $��Center=�$��Center=�.+��� -   $ 
btCancelar��$��0 0 0 10q�$��Cancelar��$	��112��$��30���� -   $ btTransferir��$��0 0 0 10q�$��
Transferir��$	��112��$��30���� -   $ btDesbloquear��$��0 0 0 10q�$��Desbloquear��$	��112��$��30���� -   $ btFecharConta��$��0 0 0 10q�$��Fechar Conta��$	��112��$��30���� -	   $ 	btServico��$��0 0 0 10q�$��Gorjeta��$	��112��$��30���� -
   $ 
btDesconto��$��0 0 0 10q�$��Desconto��$	��112��$��30���� -   $ 	btPessoas��$��0 0 0 10q�$��Pessoas��$	��112��$��30���� -   $ btJuntar��$��0 0 0 10q�$��Juntar/Separar��$	��112��$��30���� -   $ 
btImprimir��$��0 0 0 10q�$��Imprimir��$	��112��$��30���� -   $ btVoltar��$��0 0 0 10q�$��Voltar��$	��112��$��30���� $��1��$��0��$��0 2 0 2q�  
BordaClara#��[ ( �� Padding$( 2 0 0 0q�._��  �� $��25���� $��25���� $��25���� $��25���� $��25���� $��25���� $��25���� $��25���� $��25����  �� $��86 ��� $��64 �.+��� $�� Resumo��$��12(����	#40808080
���""��$
��Bold"�$��Center=�$��2��$��0 0 0 0q��� $&�� Pedidos - - - - - - - - - - - - ��$��10(�$��1��$��Center=��� $0��*Total de produtos - - - - - - - - - - - - ��$��10(�$��2��$��Center=��� $'��!Desconto - - - - - - - - - - - - ��$��10(�$��3��$��Center=��� $(��"Sub total - - - - - - - - - - - - ��$��10(�$��4��$��Center=��� $&�� Gorjeta - - - - - - - - - - - - ��$��10(�$��5��$��Center=��� $$��Total - - - - - - - - - - - - ��$��10(�$��6��$��Center=��� $/��)Tempo sem pedido - - - - - - - - - - - - ��$��10(�$��7��$��Center=��� $1��+Nº de pessoas - - - - - - - - - - - - - - ��$��10(�$��8��$��Center=��� $.��(Valor por pessoa  - - - - - - - - - - - ��$��10(�$��9��$��Center=��� -   ) ��Name$) 	lbResumo1��$��0��$��10(�$��1��$��2��$��0 0 6 0q�$��Center=�$��Right=��� -   $) 	lbResumo2��$��0��$��10(�$��2��$��2��$��0 0 6 0q�$��Center=�$��Right=��� -   $) 	lbResumo3��$��R$ 0,00��$��10(�$��3��$��2��$��0 0 6 0q�$��Center=�$��Right=��� -   $) 	lbResumo4��$��R$ 0,00��$��10(�$��4��$��2��$��0 0 6 0q�$��Center=�$��Right=��� -   $) 	lbResumo5��$��R$ 0,00��$��10(�$��5��$��2��$��0 0 6 0q�$��Center=�$��Right=��� -   $) 	lbResumo6��$��R$ 0,00��$��10(�$��6��$��2��$��0 0 6 0q�$��Center=�$��Right=��� -   $) 	lbResumo7��$��00:00��$��10(�$��7��$��2��$��0 0 6 0q�$��Center=�$��Right=��� -   $) 	lbResumo8��$��0��$��10(�$��8��$��2��$��0 0 6 0q�$��Center=�$��Right=��� -   $) 	lbResumo9��$��R$ 0,00��$��10(�$��9��$��2��$��0 0 6 0q�$��Center=�$��Right=�