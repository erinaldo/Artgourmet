   M S B A M L   `   `   `  ����T  PArtebit.Restaurante.Caixa, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Artebit.Restaurante.CaixaArtebit.Restaurante.Caixa  5   	   5    /Artebit.Restaurante.Caixa.Cadastro.FormProdutos   5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5   	   9local'clr-namespace:Artebit.Restaurante.Caixa  
   5   	   @d2http://schemas.microsoft.com/expression/blend/2008  
   5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   5   	   $	��400��5	   	   $	��600��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5
   	      CanMove . $
��Mesa��5   	   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5   	   &   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5   	    � 	Resources 5      +  %Artebit.Restaurante.Caixa.BinaryImage   ConversorImg��%   &	          5   	   6   5      .��5      � $��1q�6    � RowDefinitions 5   
   �� 5      $	��25* �6   �� 5      $
��289* �6   �� 5      $��50 �6   ��5   
   �� 5       �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��6   .+�5   
   ,  &Telerik.Windows.Controls.RadTabControl  -   5          Name$ radTabControl1��5   I   $��Stretch=�6    $��0q�6>   $��Stretch=�6a   ;  5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=�6}   $��2��6�   .@�5      )  #Telerik.Windows.Controls.RadTabItem  -   5        	  Name$	 RadTabEditar��5   _   $��Identificação��6!   $��30��68   $
��Left=�6D   $	��Top=�6u   $	��120��6�   ���#EEEEEE.��5       � $��0 20 0 0q�5!       5#      �� 5$      $��40 �6)   �� 5%      �� 5&      �� 5'      �� 5(      $��40 �6)   �� 5)      $��40 �6)   
 � ColumnDefinitions
 5+      �� 5,      �� 5-      $	��220 �6+   �� 5.      �� 5/      $��4* �6+   .+�52      �� $��Código:��6    $��0��53      $��Center=�54      $��Center=�55      /	  )Telerik.Windows.Controls.RadMaskedTextBox	  56      -   5         	 Name$ 	txtCodigo��57      $��1��58      $��0��59      $��25��5:       	  MaskType$
 None=�5;      $	��100��5<      $
��Left=�5=      �� 5?      $��Tipo:��6    $��0��5@      $��2��5A      $��Center=�5B      $��Center=�5C      � 5E      $��0��6    $��3��5F      .+�5H      	� -   5         	�Name$ check1��5I      $��Produto��5H   &   $	��100��5J      $
��Left=�5K       	� 	GroupName$
 Tipo��5L       d� 	IsChecked$
 TrueY�5M      $��25��5N      	� 5P      -   5        $ check2��5Q      $��Insumo��5P   &   $	��100��5R      $
��Left=�5S      $
 Tipo��5T      $��25��5U      	� 5X      -   5        $ check3��5Y      $��
Sub-Insumo��5X   &   $	��100��5Z      $
��Left=�5[      $
 Tipo��5\      $��25��5]      5_      �� 5b      $��Nome:��6    $��1��5c      $��Center=�5d      $��Center=�5e      �� 5f      -   5         ��Name$ txtNome��5k      $��1��5f      $��3��5g      $��1��5h      $��25��5i      $
��Left=�5j      $��Center=�5l      $	��450��5m      �� 5o      $��Ativo:��6    $��2��5p      $��Center=�5q      $��Center=�5r      �� 5s      -   5         ��Name$ 
checkAtivo��5u      $��1��5s      $��2��6/   $��Center=�5t      $
 trueY�6+   $��2��5u   $   $ ��0.333,21.667,60.333,12.667q�68   �� 5x      $��Unidade Controle:��6    $��2��5y      $��1��5z      $��2��5{      $��0 0 15 0q�5|       $��Right=�5}      $��Center=�5~      *
  $Telerik.Windows.Controls.RadComboBox
  5      -	   5         
 Name$ cmbUndControle��5�   !   $��2��5   *   $��3��5�   !   $
��Left=�5�   !   $	��170��5�   *    �� SelectedValuePath$ codUnd��5�   *    �� DisplayMemberPath$ 	descricao��5�   *   $��Center=�5�   !   �� 5�      $��Validade (dias):��6    $��3��5�      $��0��5�      $��2��5�       $��15 0 0 0q�5�       $
��Left=�5�      $��Center=�5�      	  5�      -
   5        $ txtValidade��5�   /   $��3��5�   /   $��1��5�   /   $
��Left=�5�   /   $ Numeric=�5�   /    	  Mask$ D��6B   $��25��5�   /   $��50��5�   /   $��50 0 0 0q�5�   /   �� 5�      $��Tipo:��6    $��3��5�      $��2��5�      $��Center=�5�      $��Center=�5�      � 5�      $��3��6    $��3��5�      .+�5�      	� -   5        $ tipo1��5�      $��	Tributado��5�   &   $��75��5�      $
��Left=�5�      $ TipoT��5�      $
 TrueY�5�      $��25��5�      	� 5�      -   5        $ tipo3��5�      $��Sub Tributado��5�   &   $	��100��5�      $
��Left=�5�      $ TipoT��5�      $��25��5�      	� 5�      -   5        $ tipo2��5�      $��Isento��5�   &   $��75��5�      $
��Left=�5�      $ TipoT��5�      $��25��5�      	� 5�      -   5        $ tipo4��5�      $��Não Tributado��5�   &   $	��100��5�      $
��Left=�5�      $ TipoT��5�      $��25��5�      5�      �� 5�      $��
Alíquota:��6    $��4��5�      $��2��5�      $��0 0 15 0q�5�       $
��Left=�5�      $��Center=�5�      
  5�      -   5        $ cmbAliquota��5�   !   $��4��5�   *   $��3��5�   !   $
��Left=�5�   !   $	��170��5�   *   $ id��5�   *   $
 nome��5�   *   $��Center=�5�   !   �� 5�      $��	Preço 1:��6    $��5��5�      $��0��5�      $��Center=�5�      $��Center=�5�      	  5�      -   5        $ 	txtPreco1��5�   /   $��5��5�   /   $��1��5�   /   $
��Left=�5�   /   $ Numeric=�5�   /   $ f4��6B   $��25��5�   /   $��70��5�   /   �� 5�      $��	Preço 2:��6    $��5��5�      $��1��5�      $��70 0 0 0q�5�       $��Center=�5�      $��Center=�5�      	  5�      -   5        $ 	txtPreco2��5�   /   $��5��5�   /   $��1��5�   /   $��2��5�   /   $��Right=�5�   /   $ Numeric=�5�   /   $ f4��6B   $��0 0 35 0q�5�   /   $��25��5�   /   $��70��5�   /   �� 5�      $��	Preço 3:��6    $��5��5�      $��3��5�      $��25 0 0 0q�5�       $
��Left=�5�      $��Center=�5�      	  5�      -   5        $ 	txtPreco3��5�   /   $��5��5�   /   $��3��5�   /   $
��Left=�5�   /   $ Numeric=�5�   /   $ f4��6B   $��25��5�   /   $��70��5   /   $��80 0 0 0q�5  /   5     5       5
     -   5        $	 RadTabAddDetalhes��5
  [   $��Detalhes��6!   $��30��64   $
��Left=�6@   $	��Top=�6v   $	��120��6�   ���#EEEEEE.��5     � $��20q�6    5     �� 5     $��40 �6)   �� 5     �� 5     �� 5     �� 5     $��40 �6)   �� 5     $��40 �6)   
 5     �� 5     �� 5     $	��220 �6+   �� 5     �� 5     $��4* �6+   .+�5     �� $��2��6!   $��5��65   .+�5      /  )Telerik.Windows.Controls.RadColorSelector  -   5          Name$ Selector��5!     $
��Left=�5"        MainPaletteHeaderText$ Cores do tema��5#         StandardPaletteHeaderText$ Cores Padrão��5$         StandardPaletteVisibility$ hidden=�5%        MainPalette$ Standard=�5&        NoColorText$ Sem Cor��5'        AutomaticColor$ Transparent��63   6P   5)     �� 5+     $��Cor Selecionada:��6    $��1��5,     $��2��5-     $��0 0 15 0q�5.     $��right=�5/     $��Center=�50     �� 52     -   5        $��	Retangulo��52      $��25��61   $��25��6<   $��3��6H   $
��left=�6X   ��53     �� 54      �� Color$ Transparent��6.   56     �� 58     $��Ordem Cardápio:��6(   $��1��59     $��2��5:     $��1��5;     $��0 0 15 0q�5<     $��right=�5=     $��Center=�5>     	  5?     -   5        $ txtOrdemCardapio��5H      	  	MaxLength$ 3��5@     $��3��5A     $��3��5B     $��1��5C     $ Numeric=�5D     $ d��5E     $��25��5F     $
��Left=�5G     $��Center=�5I     $��50��5J     �� 5L     $��Nome Cardápio:��6    $��1��5M     $��2��5N     $��2��5O     $��0 0 15 0q�5P     $��right=�5Q     $��Center=�5R     �� 5S     -   5        $ txtNomeResumido��5Y     $��3��5T     $��3��5U     $��2��5V     $��25��5W     $
��Left=�5X     $��Center=�5Z     $	��150��5[     �� 5]     $��Código Mobile:��6    $��1��5^     $��2��5_     $��3��5`     $��0 0 15 0q�5a     $��right=�5b     $��Center=�5c     �� 5d     -   5        $ 	txtMobile��5j     $��3��5e     $��3��5f     $��3��5g     $��25��5h     $
��Left=�5i     $��Center=�5k     $	��150��5l     5o     5p       5r     -   5        $	 RadTabAddImpressoras��5r  h   $��Plano(s) de fidelidade��6!   $��30��6A   $
��Left=�6M   $	��Top=�6�   $	��130��6�   ���#EEEEEE.��5s     �  5t       Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow  �� Property! �� Value" �� 
TargetType# ��Name��%�  &	       �� 5u     "  64   .�5v     ��   � ��$! 25��67   �� 5w       � ��! 5x  "   �� 5y  &   ""  66   .R�5{  *   �� $# borda1��61   ���#B68944���#49181A$��0 0 0 0q�5~  %   ._�5  .   � 
 5�  2   �� 5�  6   $��70 �6G   �� 5�  6   $��* �6G   �� 5�  6   $	��100 �6G   .+�5�  2   �� ���#49181A$��1q�5�  -   $��0��5�  -   ._�5�  6   �� $��0 0 0 0q�6<   
�������$��Center=�5�  4   $��Center=�5�  4   $��11(�5�  4   $��Código��5�  4   5�  3   �� 5�  2   ���#49181A$��1q�5�  -   $��1��5�  -   ._�5�  6   �� $��Center=�6<   
�������$��Center=�5�  4   $��11(�5�  4   $
��Nome��5�  4   5�  3   �� 5�  2   ���#49181A$��1q�5�  -   $��2��5�  -   ._�5�  6   �� $��Center=�6<   
�������$��Center=�5�  4   $��11(�5�  4   $��Pontos por real��5�  4   5�  3   5�  /   5�  +   5�  '   5�     5�     5�     .+�5�     *  $Telerik.Windows.Controls.RadGridView  -   5        $  Name$$ gridFidelidade��5�  *   $	��280��6B   $	��280��6R   $	��580��6_   $��0��6k   $��2��6{   % J� Opacity$	% 0.9Q�6�   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl&   CanUserFreezeColumns& .  '   RowIndicatorVisibility$' 	Collapsed=�5�  ;   (   AutoGenerateColumns( .  )   
IsReadOnly) .  $	��Top=�5�     $��Center=�66   *   ShowGroupPanel* .  +   HeaderRowStyle#+ [ 6q   ,   Columns, 5�     1  +Telerik.Windows.Controls.GridViewDataColumn  5�     6  0Telerik.Windows.Controls.GridViewBoundColumnBase-   Header$- Código��69   -  'Telerik.Windows.Controls.GridViewColumn.   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$. 70 6t   /   IsGroupable/ . 0   IsFilterable0 . 1   DataMemberBinding1 6I   �� *IdFidelidade+6�     5�     $
- Nome��69   $. * 6j   / . 0 . 1 6G   �� *Nome+6�     5�     $- Pont os por real��69   $	. 100 6w   / . 0 . 1 6S   �� *Valor+6�   5�     5�     5�     5�     �� 5�  
   $��Bottom=�6   $��1��6,   
�������$	��590��6Q   $��55��6]   $��0,0,-4,0q�6i   $��2��6{   ._�5�     � .+�5�     (  "Telerik.Windows.Controls.RadButton  -   5        2  Name$2 	btnSalvar��5�  $   $��Salvar��67   $��0 0 100 -10q�6H   $��Right=�6]   $��80��6y   $��25��6�     BotaoMenor2#��[ 6�     5�     -   5        $2 btnCancelar��5�  $   $��Cancelar��69   $��
0 0 10 -10q�6L   $��Right=�6`   $��80��6|   $��25��6�   #��[ 6�   5�     5�     5�     5�     