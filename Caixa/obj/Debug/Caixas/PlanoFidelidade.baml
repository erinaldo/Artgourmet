   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa6    0Artebit.Restaurante.Caixa.Caixas.PlanoFidelidade   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5   ?   (  "Telerik.Windows.Controls.RadWindow    WindowStartupLocation$  CenterScreen=�5   @   $��Plano de Fidelidade��5   	   $	��384��6&   $	��548��63    � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter*  $Telerik.Windows.Controls.RadGridView* 
 $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5      �� 5	   
   "  6   .�5
      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � .+�5   
     -   5        $��comboPlanos��5   M   $
��Left=�6   $��
274,15,0,0q�69   $	��Top=�6`   $	��250��6x    �� DisplayMemberPath$
 nome��6�    �� SelectedValuePath$ idFidelidade��6�   5      �� 5   
   -   5        $��label1��5   [   $��Plano:��6   $��28��6!   $
��Left=�6-   $��	222,9,0,0q�6H   $	��Top=�6i   $��14(�6�     5   
   -   5        $��gridPrincipal��5   L   $
��Left=�6   $��	0,137,0,0q�69   $	��Top=�6a   $	��536��6y   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   IsFilteringAllowed .     AutoGenerateColumns .  	   ShowGroupPanel	 .  $	��178��6�   
   SelectionMode$
 Extended=�6�      Columns 5      3  -Telerik.Windows.Controls.GridViewSelectColumn  5      1	  +Telerik.Windows.Controls.GridViewDataColumn	  5      6
  0Telerik.Windows.Controls.GridViewBoundColumnBase 
  Header$ Plano��6-   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ * 6<    
  DataMemberBinding 6F   �� *AFIDELIDADE.nome+	  5      $	 CPF��6-   $	 140 6:    6F   �� *cpf+	  5      $ Valor��6-   $	 100 6<    6H   �� *valor+ �� StringFormat$ {0:c}��5        5   
   -   5        $��	btExcluir��5   a   $��28��6   $��32��6(   $
��Left=�63   $��	0,110,0,0q�6N   $	��Top=�6r   ��5      �� 5      $2��,pack://application:,,,/Img/cancelarItens.png��6   $��20��6N   $��20��6Z   $
��Left=�6e   $��1q�6�   5       �� 5!   
   -   5        $��label4��5!   h   $
��CPF:��6   $��14(�6   $��28��6-   $
��Left=�69   $��
222,39,0,0q�6T   $	��Top=�6v   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5"   
   -   5        $��txtCpf��5"   �   $��Right=�6#   $��	0,45,12,0q�6?      Mask$ ###.###.###-##��6R      MaskType$ Standard=�6h      
MaskedText$  ��6|      TextAlignment$ Right=�6�   $	��Top=�6�   $	��250��6�   �� 5$   
   -   5        $��label2��5$   ]   $��Valor:��6   $��28��6!   $��Right=�6-   $��
0,69,269,0q�6I   $	��Top=�6k   $��14(�6�   / 
 )Telerik.Windows.Controls.RadNumericUpDown  5%   
   -   5        $��txtValor��5%   R   $
��Left=�6#   $��
274,73,0,0q�6>   $	��Top=�6b   $	��250��6z      ValueFormat$ Currency=�6�   +  %Telerik.Windows.Controls.RadRangeBase   Minimum$ 0Q�6�      Maximum$ 	100000000Q�6�     5&   
   -	   5        $��btnAdicionar��5&   n   $��	Adicionar��6   $��25��60   $��Right=�6<   $��0,107,12,218q�6X      BotaoMenor2#��[  6�   $��80��6�   6�   � 5'   
   $��	0,317,0,0q�6   .+�5(        -
   5        $��btnOK��5(   |   $��OK��64   $��25��6A   $��Right=�6M   $��	0 0 100 0q�6i   #��[  6�   $��80��6�     5)      -   5        $��btnCancelar��5)   �   $��Cancelar��6:   $��25��6M   $��Right=�6Y   $��0 0 10 0q�6u   #��[  6�   $��80��6�   5*      �� 5+   
   -   5        $��image1��5+   I   $��88��6   $
��Left=�6   $��59,7,0,0q�67   $��Uniform=�6W   $	��Top=�6i   $��91��6�   $A��;/Artebit.Restaurante.Caixa;component/Img/databaseconfig.png��6�   5,      5-      