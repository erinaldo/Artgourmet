   M S B A M L   `   `   `  ���� T  PArtebit.Restaurante.Caixa, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Artebit.Restaurante.Caixa.Controles#Artebit.Restaurante.Caixa.Controles  -    'Artebit.Restaurante.Caixa.Caixas.Balcao   ?	Controles1clr-namespace:Artebit.Restaurante.Caixa.Controles  m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   @d2http://schemas.microsoft.com/expression/blend/2008  
   -   .���� .+�� .+�4   .Artebit.Restaurante.Caixa.Controles.Cabecalho1  -      Name$  	Cabecalho��   Titulo$ Caixa / Balcão��   BotaoInicioEvento .  � $	��630��$	��630�� � ColumnDefinitions �� $	��270��$	��260 ��� $
��470* ��� $	��247��$	��273 � � RowDefinitions �� $��30 ��� �� $	��250 �.+��� -   $��lblmesa��$��	Comanda: ��$��Arial)�$��15(�$
��Bold"�$
��Left=�$	��Top=�
�������$��10,3,0,0q�$��80��$��18��$��2��$��1���� -   $��txtNumeroBusca��$��2��$��1��$��80��$��25��$��85,0,0,0q�$
��Left=�$	��Top=�|�.  J� Opacity$	 0.9Q�(  "Telerik.Windows.Controls.RadButton  -   $��btBuscar��$��2��$��1��$��65��$��25��$��0,0,12,0q�$��Right=�$	��Top=�   BotaoMenor1#��[  $	 0.9Q�.��
Procurar� $��2��$��1��$	��310��$��7,0,1,7q�$��Bottom=�.+���  �� CornerRadius$ 3��
�������
�������$��1q�$	 0.9Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$
 Gray��	   Opacity$	 1Q�
   ShadowDepth$
 3Q�� $��1,0,-1,0q� �� $��40���� $��27���� $��27���� $��27���� $��27���� $��27���� $��27���� $��27���� $��27����  �� $
��135* ��� $	��95* �.+��� $�� Resumo��$��18(����#DDD
���""��$��Segoe UI)�$��2��$��1 7 2 7q��� $*��$Itens - - - - - - - - - - - - - - - ��$��15(�$��1��$��Center=�$��7q��� $*��$Hora Inicial- - - - - - - - - - - - ��$��15(�$��2��$��Center=�$��7q��� $*��$Permanencia - - - - - - - - - - - - ��$��15(�$��3��$��Center=�$��7q��� $��Cliente:��$��15(�$��4��$��Center=�$��7q��� -    ��Name$ 	lbResumo1��$��0��$��15(�$��1��$��2��$��Center=�$��7q��� -   $ 	lbResumo2��$��00:00��$��15(�$��2��$��2��$��Center=�$��7q��� -   $ 	lbResumo3��$��00:00��$��15(�$��3��$��2��$��Center=�$��7q��� -	   $ 	lbResumo4��$�� ��$��15(�$��4��$��2��$��Center=�$��7q�� $��2��$��2��$	��236��$��7,0,1,7q�$��Bottom=�- 
 'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=�.+��� $ 3��
�������
�������$��1q�$	 0.9Q�   $
 Gray��$	 1Q�$
 3Q��� $�� Itens Agrupados��$��18(����#DDD
���""��$��Segoe UI)�$��2��$��1 7 2 7q�$	��Top=�$��27��� $��1 36 0 0q� � ShowGridLines . $	��190�� � 	Resources   Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property �� Value �� 
TargetType ��Name��%�  &	       �� "  .���  � ��$ 25����  � �� �� "  .R��� $ borda1�����#B68944���#49181A$��0 0 0 0q�._��  �� $	��130 ��� $	��100 �.+��� ���#49181A$��1q�$��0��._��� $��0 0 0 0q�
�������$��Center=�$��13(�$��Descrição���� ���#49181A$��1q�$��1��._��� $��Center=�
�������$��Center=�$��13(�$��Qtd X Preço��.+��� $��3��*  $Telerik.Windows.Controls.RadGridView  -
     Name$ 
gridResumo��$
��auto��$	��230��$��Disabled=�$	 0.7Q�;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�   AutoGenerateColumns .  $��0��   
IsReadOnly .    ShowGroupPanel .     GridLinesVisibility$ 
Horizontal=�   Columns 1  +Telerik.Windows.Controls.GridViewDataColumn  6	  0Telerik.Windows.Controls.GridViewBoundColumnBase 	  Header$ Descrição��-
  'Telerik.Windows.Controls.GridViewColumn 
  Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 130  
  IsGroupable .   
  IsFilterable .    	  DataMemberBinding  �� *	descricao+  $ Qtd X Preço��$ 90  .  .    �� *qtdval+� $��2��$	��236��$��8,0,9,7q�$��Bottom=�.+��� $ 3��
�������
�������$��1q�$	 0.9Q�   $
 Gray��$	 1Q�$
 3Q��� $�� Totais��$��18(����#DDD
���""��$��Segoe UI)�$��2��$��1 7 2 7q�$	��Top=�$��27��� $��1 36 0 0q�$	��190�� �� $��27���� $��27���� $��27���� $��27���� $��27���� $��27����  �� $
��119* ��� $
��110* �.+��� $*��$Itens - - - - - - - - - - - - - - - ��$��15(�$��0��$��Center=�$��7q��� $*��$Desconto- - - - - - - - - - - - - - ��$��15(�$��1��$��Center=�$��7q��� $��Total - - - - - ��$��17(�$��6��$
��Bold"�$��Center=�$��7q��� -   $ lbTotal1��$��0��$��15(�$��0��$��2��$��Center=�$��7q��� -   $ lbTotal2��$��R$ 0,00��$��15(�$��1��$��2��$��Center=�$��7q��� -   $ lbTotal4��$��R$ 99.999,00��$��17(�$��6��$��2��$
��Bold"�$��Center=�$��6q�� $��1��$��2��$��7 7 7 7q�.+��� $ 3��
�������
�������$��1q�$	 0.9Q�   $
 Gray��$	 1Q�$
 3Q�� $��1��$��2��$��	20 7 20 7q�$	��450�� �� �� �� �� ��  �� $��42 ��� $��5* ��� $��42 ��� $��5* �.+��� $��0��$��0��$��Center=�$��35��$
��Left=�$��40��.+��� ! �� Left$! 0��
" �� Top$" 3��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $��F2��$" 12��$! 10��$
��Bold"�$
��16px(�$��Center=��� -   # ��Name$# btnAddItens��$��40��$	��160��$
��Left=�$��1��
������ 
������ $��0q�
���   �$��0 0 0 0q�{�.  .��� $��40��$	��160�� �� $
��33px ��� .+��� $3��-pack://application:,,,/Img/adicionarItens.png��$��30��$��30��$
��Left=�$��0��$	��Top=�$��0 4 0 0q��� -   $��txtAddItens��$��Adicionar Itens��$��Center=�$��1�����#AAAAAA$��Segoe UI)�$
��15px(��� $��0��$��2��.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $��F7��$" 7��$! 10��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -   $# btnCancelItens��$��40��$	��160��$
��Left=�$
��Left=�$��3��$��0��$��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�
���   �$��0q�{�.  .��� $��40��$	��160�� �� $
��33px ��� .+��� $2��,pack://application:,,,/Img/cancelarItens.png��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -   $��txtCancelItens��$��Cancelar Itens��$��Center=�$��Segoe UI)�$
��15px(�$��1��$��0 0 0 0q����#AAAAAA�� $��1��$��0��$	��Top=�$��35��$
��Left=�.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $��F3��$" 7��$! 10��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -   $# btnTransferir��$��40��$	��160��$
��Left=�$
��Left=�$��1��$��1��{�.  $��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�
���   �.��� $��40��$	��160�� �� $
��33px ��� .+��� $/��)pack://application:,,,/Img/transferir.png��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -   $ txtTransferir��$��
Transferir��$��Center=�$��1��$��0 0 0 0q����#AAAAAA$��Segoe UI)�$
��15px(��� $��1��$��2��.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $��F8��$" 7��$! 10��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -   $# btnCancelarConta��$��40��$	��160��$
��Left=�$
��Left=�$��3��$��1��{�.  $��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�
���   �.��� $��40��$	��160�� �� $
��33px ��� .+��� $0��*pack://application:,,,/Img/fecharConta.png��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -   $ txtCancelaConta��$��Cancelar Conta��$��Center=�$��Segoe UI)�$
��15px(�$��1��$��0q����#AAAAAA�� $��2��$��0��.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $��F4��$" 7��$! 10��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -   $# btnDesconto��$��40��$	��160��$
��Left=�$
��Left=�$��1��$��2��{�.  $��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�
���   �.��� $��40��$	��160�� �� $
��33px ��� .+��� $-��'pack://application:,,,/Img/desconto.png��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -   $ txtDesconto��$��Desconto��$��Center=�$��Segoe UI)�$
��15px(�$��1��$��0q����#AAAAAA�� $��2��$��2��.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $��F9��$" 7��$! 10��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -   $# btnNovo��$��40��$	��160��$
��Left=�$
��Left=�$��3��$��2��{�.  $��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�
���   �.��� $��40��$	��160�� �� $
��33px ��� .+��� $3��-pack://application:,,,/Img/adicionarItens.png��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -   $ txtNovo��$��Nova Comanda��$��Center=�$��Segoe UI)�$
��15px(�$��1��$��0q����#AAAAAA�� $��3��$��0��.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $��F5��$" 7��$! 10��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -   $# 
btnCliente��$��40��$	��160��$
��Left=�$
��Left=�$��1��$��3��{�.  $��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�
���   �.��� $��40��$	��160�� �� $
��33px ��� .+��� $,��&pack://application:,,,/Img/pessoas.png��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -   $ 
txtCliente��$��Cliente��$��Center=�$��Segoe UI)�$
��15px(�$��1��$��0q����#AAAAAA�� $��3��$��2��.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $	��F10��$" 7��$! 7��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -   $# btnFechaConta��$��40��$	��160��$
��Left=�$
��Left=�$��3��$��3��$��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�$��0q�$��0q�
���   �$ � ForceCursor$ . {�.  .��� $��40��$	��160�� �� $
��33px ��� .+��� $*��$pack://application:,,,/Img/pagar.png��$��0��$��0��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -   $ txtFechaConta��$��Fechar Conta��$��1��$��0��$��Segoe UI)�$
��15px(�$��0q����#AAAAAA$��Bottom=�$��30���� $��4��$��0��$y�Hidden=�.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $��F6��$" 7��$! 10��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -   $# btnBloquear��$��40��$	��160��$
��Left=�$y�Hidden=�$
��Left=�$��1��$��4��{�.  $��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�
���   �.��� $��40��$	��160�� �� $
��33px ��� .+��� $-��'pack://application:,,,/Img/bloquear.png��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -   $ txtBloquear��$��Bloquear��$��Center=�$��Segoe UI)�$
��15px(�$��1��$��0q����#AAAAAA�� $��4��$��2��.+��� $! 0��$" 5��$(��"pack://application:,,,/Img/key.png��$��35��$��40��$
��Left=�$��0q��� $	��F11��$" 7��$! 7��$
��Bold"�$
��16px(�$	��Top=�$��0 5 0 0q��� -    $# 	btnOpcoes��$��40��$	��160��$
��Left=�$
��Left=�$��3��$��4��{�. $��LeftToRight=�
������ 
������ $��0q�$	 0.8Q�
���   �.��� $��40��$	��160�� �� $
��33px ��� .+��� $,��&pack://application:,,,/Img/opcoes2.png��$��30��$��30��$
��Left=�$	��Top=�$��0 4 0 0q��� -!   $ 	txtOpcoes��$��Opções��$��Center=�$��1��$��Segoe UI)�$
��15px(�$��0q�- 	 'Telerik.Windows.Controls.RadContextMenu%   ContextMenu%   -"   &  Name$& 
MenuOpcoes��'   	EventName$' Click��$	��210��' 	 !Telerik.Windows.Controls.MenuBase(   NotifyOnHeaderClick( . .@�* 	 $Telerik.Windows.Controls.RadMenuItem  -#   $��Caixa Mesas��)   CommandParameter$) 	CaixaMesa��  -$   $��Caixa Balcão��$) CaixaBalcao��  -%   $��Caixa Delivery��$) CaixaDelivery��  *   IsSeparator* . $��teste��  $��Operações Fiscais��.@�  -&   $��Abrir Período��$) AbrirPeriodo��  -'   $��Fechamento de Período��$) FecharPeriodo��  * . $��teste��  -(   $��Gerar Leitura X��$) AbrirDia��  -)   $��
Suprimento��$) 
Suprimento��  -*   $��Retiro Parcial��$) Sangria��  -+   $��Gerar Redução Z��$) 	FecharDia��  $��Relatórios��.@�  -,   $��Resumo de Vendas��$) Relatorio.ResumoVendas��  * .   --   +  Name$+ 	JutarMesa��$��Juntar Mesa��$) 
JuntarMesa��{�.    -.   $+ SepararMesa��$��Separar Mesa��$) SepararMesa��{�.    -/   $+ Imprimir��$��Imprimir PreConta��$) ImprimirPreconta��{�.    * .   $	��ECF��.@�  -0   $"��Retorna Estado da Impressora��$) RetEstadoImpressora��  -1   $(��"Imprime Configurações Impressora��$) ImprimeConfig��  -2   $��Verifica modo Operação��$) ModoOperacao��  -3   $��Cancela Último Cupom��$) CancelUltCupom��  -4   $'��!Ativa/Desativa Horário de Verão��$) HorVerao��  * .   -5   $��Ajuda��$) Ajuda��  * .   -6   $��Sobre��$) Sobre��� $��2��$��1��$��0q�.+��  , ��Name��%+  &	       �� "  .���  � ��$ 25����  � �� �� "  .R��� $ borda1�����#B68944���#49181A$��0 0 0 0q�._��  �� $��25 ��� $��60 ��� $	��120 ��� $��40 ��� $��60 ��� $��80 ��� $��70 ��� $��80 ��� $��90 ��� $��* �.+��� ���#49181A$��0q�$��0��$	 0.8Q�._��� $, 	chk_total��-7   $��Center=�$��Center=��� ���#49181A$��1q�$��1��._��� $��0 0 0 0q�
�������$��Center=�$��13(�$��Cód.���� ���#49181A$��1q�$��2��._��� $��Center=�
�������$��Center=�$��13(�$��Descrição���� ���#49181A$��1q�$��3��._��� $��Center=�
�������$��Center=�$��13(�$
��Und.���� ���#49181A$��1q�$��4��._��� $��Center=�
�������$��Center=�$��13(�$	��Qtd���� ���#49181A$��1q�$��5��._��� $��Center=�
�������$��Center=�$��13(�$��Preço���� ���#49181A$��1q�$��6��._��� $��Center=�
�������$��Center=�$��13(�$��Desconto���� ���#49181A$��1q�$��7��._��� $��Center=�
�������$��Center=�$��13(�$��Total���� ���#49181A$��1q�$��8��._��� $��Center=�
�������$��Center=�$��13(�$��Status���� ���#49181A$��1q�$��9��._��� $��Center=�
�������$��Center=�$��13(�$��Vendedor�� �� .+�  -8   $ 	gridDados��$
��auto��$	��760��-   SelectionMode$- Extended=� .  $ 	Collapsed=� .  $��0�� .  .  .   CanUserDeleteRows. .  /   CanUserInsertRows/ .  0   CanUserReorderColumns0 .  1   CanUserResizeColumns1 .  2   DragElementAction$
2 None=�3   EditTriggers$
3 None=�$��Visible=�$ 
Horizontal=� 3  -Telerik.Windows.Controls.GridViewSelectColumn    $ Cód.��$ 60  .   .    �� *codigo+  $ Descrição��$	 120  .  .   �� *nome+  $
 Und.��$ 40  .  .    �� *	unidade+  $	 Qtd��$ 60  .   .  4 	  DataFormatString$4 {0:0.00}��  �� *
quantidade+  $ Preço��$ 80  .  . $4 {0:c}��  �� *preco+  $ Desconto��$ 70  .   .  $4 {0:c}��  �� *
desconto+  $ Total��$ 80  .   .  $4 {0:c}��  �� *total+  $ Status��$ 90  .  .   �� *status+  $ Vendedor��$ *  .  .   �� *
vendedor+