   M S B A M L   `   `   `  ���� T  PArtebit.Restaurante.Caixa, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Artebit.Restaurante.CaixaArtebit.Restaurante.Caixa  U/clr-namespace:Artebit.Restaurante.Caixa.Classes!Artebit.Restaurante.Caixa.Classes  5    /Artebit.Restaurante.Caixa.Cadastro.FormProdutos   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   9local'clr-namespace:Artebit.Restaurante.Caixa  
   @d2http://schemas.microsoft.com/expression/blend/2008  
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   CClasses/clr-namespace:Artebit.Restaurante.Caixa.Classes  
   $	��400��$	��600��) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�   CanMove . $
��Mesa��( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=� � 	Resources 3  -Artebit.Restaurante.Caixa.Classes.BinaryImage   ConversorImg��%   &	          .��� $��1q� � RowDefinitions �� $	��25* ��� $
��289* ��� $��50 �����  �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��.+�, 	 &Telerik.Windows.Controls.RadTabControl  -     Name$ radTabControl1��$��Stretch=�$��0q�$��Stretch=�; 	 5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=�$��2��.@�) 	 #Telerik.Windows.Controls.RadTabItem  -     Name$ RadTabEditar��$��Identificação��$��30��$
��Left=�$	��Top=�$	��120�����#EEEEEE.��� $��0 20 0 0q� �� $��40 ��� �� �� �� $��40 ��� $��40 �	 � ColumnDefinitions	 �� �� $	��220 ��� �� $��4* �.+��� $��Código:��$��0��$��Center=�$��Center=�/  )Telerik.Windows.Controls.RadMaskedTextBox  -   
  Name$
 	txtCodigo��$��1��$��0��$��25��   MaskType$
 None=�$	��100��$
��Left=��� $��Tipo:��$��0��$��2��$��Center=�$��Center=�� $��0��$��3��.+�	� -    	�Name$ check1��$��Produto��$	��100��$
��Left=� 	� 	GroupName$
 Tipo�� d� 	IsChecked$
 TrueY�$��25��	� -   $ check2��$��Insumo��$	��100��$
��Left=�$
 Tipo��$��25��	� -   $ check3��$��
Sub-Insumo��$	��100��$
��Left=�$
 Tipo��$��25���� $��Nome:��$��1��$��Center=�$��Center=��� -    ��Name$ txtNome��$��1��$��3��$��1��$��25��$
��Left=�$��Center=�$	��450���� $��Ativo:��$��2��$��Center=�$��Center=��� -    ��Name$ 
checkAtivo��$��1��$��2��$��Center=�$
 trueY�$��2��$ ��0.333,21.667,60.333,12.667q��� $��Unidade Controle:��$��2��$��1��$��2��$��0 0 15 0q�$��Right=�$��Center=�*  $Telerik.Windows.Controls.RadComboBox  -	     Name$ cmbUndControle��$��2��$��3��$
��Left=�$	��170�� �� SelectedValuePath$ codUnd�� �� DisplayMemberPath$ 	descricao��$��Center=��� $��Validade (dias):��$��3��$��0��$��2��$��15 0 0 0q�$
��Left=�$��Center=�  -
   $
 txtValidade��$��3��$��1��$
��Left=�$ Numeric=�   Mask$ D��$��25��$��50��$��50 0 0 0q��� $��Tipo:��$��3��$��2��$��Center=�$��Center=�� $��3��$��3��.+�	� -   $ tipo1��$��	Tributado��$��75��$
��Left=�$ TipoT��$
 TrueY�$��25��	� -   $ tipo3��$��Sub Tributado��$	��100��$
��Left=�$ TipoT��$��25��	� -   $ tipo2��$��Isento��$��75��$
��Left=�$ TipoT��$��25��	� -   $ tipo4��$��Não Tributado��$	��100��$
��Left=�$ TipoT��$��25���� $��
Alíquota:��$��4��$��2��$��0 0 15 0q�$
��Left=�$��Center=�  -   $ cmbAliquota��$��4��$��3��$
��Left=�$	��170��$ id��$
 nome��$��Center=��� $��	Preço 1:��$��5��$��0��$��Center=�$��Center=�  -   $
 	txtPreco1��$��5��$��1��$
��Left=�$ Numeric=�$ f4��$��25��$��70���� $��	Preço 2:��$��5��$��1��$��70 0 0 0q�$��Center=�$��Center=�  -   $
 	txtPreco2��$��5��$��1��$��2��$��Right=�$ Numeric=�$ f4��$��0 0 35 0q�$��25��$��70���� $��	Preço 3:��$��5��$��3��$��25 0 0 0q�$
��Left=�$��Center=�  -   $
 	txtPreco3��$��5��$��3��$
��Left=�$ Numeric=�$ f4��$��25��$��70��$��80 0 0 0q�  -   $ RadTabAddDetalhes��$��Detalhes��$��30��$
��Left=�$	��Top=�$	��120�����#EEEEEE.��� $��20q� �� $��40 ��� �� �� �� $��40 ��� $��40 �	 �� �� $	��220 ��� �� $��4* �.+��� $��2��$��5��.+�/	  )Telerik.Windows.Controls.RadColorSelector	  -    	 Name$ Selector��$
��Left=� 	  MainPaletteHeaderText$ Cores do tema��  	  StandardPaletteHeaderText$ Cores Padrão��  	  StandardPaletteVisibility$ hidden=� 	  MainPalette$ Standard=� 	  NoColorText$ Sem Cor�� 	  AutomaticColor$ Transparent���� $��Cor Selecionada:��$��1��$��2��$��0 0 15 0q�$��right=�$��Center=��� -   $��	Retangulo��$��25��$��25��$��3��$
��left=�����  �� Color$ Transparent���� $��Ordem Cardápio:��$��1��$��2��$��1��$��0 0 15 0q�$��right=�$��Center=�  -   $
 txtOrdemCardapio��   	MaxLength$ 3��$��3��$��3��$��1��$ Numeric=�$ d��$��25��$
��Left=�$��Center=�$��50���� $��Nome Cardápio:��$��1��$��2��$��2��$��0 0 15 0q�$��right=�$��Center=��� -   $ txtNomeResumido��$��3��$��3��$��2��$��25��$
��Left=�$��Center=�$	��150���� $��Código Mobile:��$��1��$��2��$��3��$��0 0 15 0q�$��right=�$��Center=��� -   $ 	txtMobile��$��3��$��3��$��3��$��25��$
��Left=�$��Center=�$	��150��  -   $ RadTabAddImpressoras��$��Plano(s) de fidelidade��$��30��$
��Left=�$	��Top=�$	��130�����#EEEEEE.���    Headerstyle �� 
TargetType9
  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property  �� Value! �� 
TargetType" ��Name��%  &	       �� " 
 .���  � ��$  25����  � ��  �� "! 
 .R��� $" borda1�����#B68944���#49181A$��0 0 0 0q�._�� 	 �� $��70 ��� $��* ��� $	��100 �.+��� ���#49181A$��1q�$��0��._��� $��0 0 0 0q�
�������$��Center=�$��Center=�$��11(�$��Código���� ���#49181A$��1q�$��1��._��� $��Center=�
�������$��Center=�$��11(�$
��Nome���� ���#49181A$��1q�$��2��._��� $��Center=�
�������$��Center=�$��11(�$��Pontos por real��.+�*  $Telerik.Windows.Controls.RadGridView  -   #  Name$# gridFidelidade��$	��280��$	��280��$	��580��$��0��$��2��$ J� Opacity$	$ 0.9Q�;  5Telerik.Windows.Controls.GridView.GridViewDataControl%   CanUserFreezeColumns% .  &   RowIndicatorVisibility$& 	Collapsed=�'   AutoGenerateColumns' .  (   
IsReadOnly( .  $	��Top=�$��Center=�)   ShowGroupPanel) .  *   HeaderRowStyle#* [ +   Columns+ 1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase,   Header$, Código��-  'Telerik.Windows.Controls.GridViewColumn-   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$- 70 .   IsGroupable. . /   IsFilterable/ . 0   DataMemberBinding0 �� *IdFidelidade+  $
, Nome��$- * . . / . 0 �� *Nome+  $, Pont os por real��$	- 100 . . / . 0 �� *Valor+�� $��Bottom=�$��1��
�������$	��590��$��55��$��0,0,-4,0q�$��2��._�� .+�(  "Telerik.Windows.Controls.RadButton  -   1  Name$1 	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��  BotaoMenor2#��[   -   $1 btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[ 