   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa4    .Artebit.Restaurante.Caixa.Caixas.FecharPeriodo   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      Jd2http://schemas.microsoft.com/expression/blend/2008	     	 
    5   r   Tmc;http://schemas.openxmlformats.org/markup-compatibility/2006	     	 
    6�   &   Telerik.Windows.Controls.Theming    Theme-  'Telerik.Windows.Controls.ThemeConverter$  Transparent 5   P   $��Fechar Período��5      )  #Telerik.Windows.Controls.WindowBase   
ResizeMode$ NoResize=�6&   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6<   $	��750��6�   $	��490��6  
������� � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value*  $Telerik.Windows.Controls.RadGridView* 
 $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5	      �� 5
   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � .+�5   
     -   5        $��gridPagamentos��5   `   $��2��6   $
��Left=�62   $��	12,35,0,0q�6M   $	��Top=�6v   ;	  5Telerik.Windows.Controls.GridView.GridViewDataControl 	  ShowGroupPanel .  $	��350��6�    	  IsScrolling .  $	��349��6�   	 	  AutoGenerateColumns	 .  
 	  IsFilteringAllowed
 .   	  EditTriggers$ 	CellClick=�6	   	  GridLinesVisibility$
 Both=�6"   � UseLayoutRounding .  5         ValorPagamento/
 
 )Telerik.Windows.Controls.RadNumericUpDown 
  ValueFormat+  %Telerik.Windows.Controls.RadRangeBase   Minimum   Maximum��%h   &	        �� 5      .R�5      
  $ Currency=�6/   $ 0Q�6F   $ 	100000000Q�6R   5      5       	  Columns 5      1  +Telerik.Windows.Controls.GridViewDataColumn  5      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Forma Pagamento��6-   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$
 300* 6F      
IsReadOnly .    DataMemberBinding 6S   �� *AFORMAPGTO.descricao+6�     5      $ Valor��6-   $
 100* 6<    6I   �� *valorRecebido+ �� StringFormat$ {0:c}��5      5        5   
   -   5        $��gridRecebimentos��5   M   $
��Left=�6   $��
380,35,0,0q�69   $	��Top=�6e    .  $	��350��6�    .  $	��349��6�   	 .  
 .  $ 	CellClick=�6�   $
 Both=�6   . $��1��6t   5       ��%h   &	        �� 5!      .R�5"      
  $ Currency=�6/   $ 0Q�6F   $ 	100000000Q�6R   5#      5$       5%        5&      $ Forma Pagamento��6-   $
 200* 6F    .  6S   �� *AFORMAPGTO.descricao+6�     5'      $ Valor��6-   $	 100 6<    6H   �� *valorRecebido+$ {0:c}��5(        5)      $ Excluir��6-   $ 45 6>    .    CellTemplate 5*      �� 5+      .R�5,        $��	btExcluir��6s   -   5        $��28��5,   0   $��32��6<   $
��Left=�6G   $��0,0,0,0q�6b   $	��Top=�6�   ��5-   "   �� 5.   &   $2��,pack://application:,,,/Img/cancelarItens.png��6,   $��20��6b   $��20��6n   $
��Left=�6y   $��1q�6�   50      51      53      55      �� 5F   
   $��Bottom=�6   ���#EFEFEF$	��750��6A   $��35��6M   $��2��6Y   $��0,0,0,0q�6m   ._�5G      � .+�5H        -   5        $��btnOK��5H   $   $��OK��61   $��	0 0 110 0q�6>   $��Right=�6Q   $��80��6m   $��25��6x     BotaoMenor2#��[ 6�     5I      -   5        $��btnCancelar��5I   $   $��Cancelar��67   $��0 0 20 0q�6J   $��Right=�6\   $��80��6x   $��25��6�   #��[ 6�   5K      5L      5M      5N      