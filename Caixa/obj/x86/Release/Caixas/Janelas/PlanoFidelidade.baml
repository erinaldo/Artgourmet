   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa>    8Artebit.Restaurante.Caixa.Caixas.Janelas.PlanoFidelidade   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    (  "Telerik.Windows.Controls.RadWindow    WindowStartupLocation$  CenterScreen=�$��Plano de Fidelidade��$	��384��$	��548�� � 	Resources (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter*  $Telerik.Windows.Controls.RadGridView* 
 $Telerik.Windows.Controls.RadComboBox��%�   '        '  /     '  [     �� "  .���  �@ Theme$ Windows7 �� "  .���  �@ Theme$ Metro �� "  .���  �@ Theme$ Summer .��� .+�  -   $��comboPlanos��$
��Left=�$��
274,15,0,0q�$	��Top=�$	��250�� �� DisplayMemberPath$
 nome�� �� SelectedValuePath$ idFidelidade���� -   $��label1��$��Plano:��$��28��$
��Left=�$��	222,9,0,0q�$	��Top=�$��14(�  -   $��gridPrincipal��$
��Left=�$��	0,137,0,0q�$	��Top=�$	��536��;  5Telerik.Windows.Controls.GridView.GridViewDataControl   IsFilteringAllowed .     AutoGenerateColumns .  	   ShowGroupPanel	 .  $	��178��
   SelectionMode$
 Extended=�   Columns 3  -Telerik.Windows.Controls.GridViewSelectColumn  1	  +Telerik.Windows.Controls.GridViewDataColumn	  6
  0Telerik.Windows.Controls.GridViewBoundColumnBase 
  Header$ Plano��-  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ *  
  DataMemberBinding �� *AFIDELIDADE.nome+	  $	 CPF��$	 140  �� *cpf+	  $ Valor��$	 100  �� *valor+ �� StringFormat$ {0:c}��  -   $��	btExcluir��$��28��$��32��$
��Left=�$��	0,110,0,0q�$	��Top=����� $2��,pack://application:,,,/Img/cancelarItens.png��$��20��$��20��$
��Left=�$��1q��� -   $��label4��$
��CPF:��$��14(�$��28��$
��Left=�$��
222,39,0,0q�$	��Top=�/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   $��txtCpf��$��Right=�$��	0,45,12,0q�   Mask$ ###.###.###-##��   MaskType$ Standard=�   
MaskedText$  ��   TextAlignment$ Right=�$	��Top=�$	��250���� -   $��label2��$��Valor:��$��28��$��Right=�$��
0,69,269,0q�$	��Top=�$��14(�/ 
 )Telerik.Windows.Controls.RadNumericUpDown  -   $��txtValor��$
��Left=�$��
274,73,0,0q�$	��Top=�$	��250��   ValueFormat$ Currency=�+  %Telerik.Windows.Controls.RadRangeBase   Minimum$ 0Q�   Maximum$ 	100000000Q�  -	   $��btnAdicionar��$��	Adicionar��$��25��$��Right=�$��0,107,12,218q�   BotaoMenor2#��[  $��80��� $��	0,317,0,0q�.+�  -
   $��btnOK��$��OK��$��25��$��Right=�$��	0 0 100 0q�#��[  $��80��  -   $��btnCancelar��$��Cancelar��$��25��$��Right=�$��0 0 10 0q�#��[  $��80���� -   $��image1��$��88��$
��Left=�$��59,7,0,0q�$��Uniform=�$	��Top=�$��91��$A��;/Artebit.Restaurante.Caixa;component/Img/databaseconfig.png��