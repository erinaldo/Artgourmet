   M S B A M L   `   `   `  ����T  PArtebit.Restaurante.Caixa, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Artebit.Restaurante.CaixaArtebit.Restaurante.Caixa  5   K   U/clr-namespace:Artebit.Restaurante.Caixa.Classes!Artebit.Restaurante.Caixa.Classes  5      5    /Artebit.Restaurante.Caixa.Cadastro.FormProdutos   5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5      9local'clr-namespace:Artebit.Restaurante.Caixa  
   6K   @d2http://schemas.microsoft.com/expression/blend/2008  
   5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   5      CClasses/clr-namespace:Artebit.Restaurante.Caixa.Classes  
   5      $	��400��5   N   $	��600��6[   ) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6g      CanMove . $
��Mesa��5      ( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6"    � 	Resources 5	      3  -Artebit.Restaurante.Caixa.Classes.BinaryImage   ConversorImg��%   &	          5
   
   6   5      .��5      � $��1q�6    � RowDefinitions 5   
   �� 5      $	��25* �6   �� 5      $
��289* �6   �� 5      $��50 �6   ��5   
   �� 5       �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��6   .+�5   
   , 	 &Telerik.Windows.Controls.RadTabControl  -   5          Name$ radTabControl1��5   I   $��Stretch=�6    $��0q�6>   $��Stretch=�5       ; 	 5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=�6<   $��2��6]   .@�5      ) 	 #Telerik.Windows.Controls.RadTabItem  -   5          Name$ RadTabEditar��5   _   $��Identificação��6!   $��30��68   $
��Left=�6D   $	��Top=�5   !   $	��120��69   ���#EEEEEE.��5      � $��0 20 0 0q�6    5      �� 5      $��40 �6(   �� 5       �� 5!      �� 5"      �� 5#      $��40 �6(   �� 5$      $��40 �6(   	 � ColumnDefinitions	 5&      �� 5'      �� 5(      $	��220 �6+   �� 5)      �� 5*      $��4* �6+   .+�5-      �� $��Código:��6    $��0��6/   $��Center=�6<   $��Center=�6Y   /  )Telerik.Windows.Controls.RadMaskedTextBox  5.      -   5        
  Name$
 	txtCodigo��5.   /   $��1��6B   $��0��6R   $��25��6_      MaskType$
 None=�5/   /   $	��100��6?   $
��Left=�6K   �� 51      $��Tipo:��6    $��0��6-   $��2��6:   $��Center=�6J   $��Center=�52       � 54      $��0��6    $��3��6-   .+�56      	� -   5         	�Name$ check1��56   8   $��Produto��6&   $	��100��6H   $
��Left=�6T    	� 	GroupName$
 Tipo��57   &    d� 	IsChecked$
 TrueY�67   $��25��6H   	� 59      -   5        $ check2��59   7   $��Insumo��6&   $	��100��6G   $
��Left=�6S   $
 Tipo��5:   &   $��25��67   	� 5=      -   5        $ check3��5=   ;   $��
Sub-Insumo��6&   $	��100��6K   $
��Left=�6W   $
 Tipo��5>   &   $��25��67   5@      �� 5C      $��Nome:��6    $��1��6-   $��Center=�6:   $��Center=�6W   �� 5D      -   5         ��Name$ txtNome��5E      $��1��5D      $��3��6.   $��1��6B   $��25��6O   $
��Left=�6[   $��Center=�5E   /   $	��450��6J   �� 5G      $��Ativo:��6    $��2��6.   $��Center=�6;   $��Center=�6X   �� 5H      -   5         ��Name$ 
checkAtivo��5I      $��1��5H      $��2��6/   $��Center=�6<   $
 trueY�6W   $��2��5I   3   $ ��0.333,21.667,60.333,12.667q�6G   �� 5L      $��Unidade Controle:��6    $��2��69   $��1��6F   $��2��6V   $��0 0 15 0q�5M       $��Right=�62   $��Center=�6N   *  $Telerik.Windows.Controls.RadComboBox  5N      -	   5          Name$ cmbUndControle��5N   G   $��2��6*   $��3��67   $
��Left=�5O   *   $	��170��6E    �� SelectedValuePath$ codUnd��6Q    �� DisplayMemberPath$ 	descricao��6l   $��Center=�5P   *   �� 5S      $��Validade (dias):��6    $��3��68   $��0��6E   $��2��6U   $��15 0 0 0q�5T       $
��Left=�62   $��Center=�6M     5U      -
   5        $
 txtValidade��5U   /   $��3��6D   $��1��6Q   $
��Left=�5V   /   $ Numeric=�6J      Mask$ D��6]   $��25��6f   $��50��6r   $��50 0 0 0q�6}   �� 5X      $��Tipo:��6    $��3��6-   $��2��6:   $��Center=�6J   $��Center=�5Y       � 5[      $��3��6    $��3��6-   .+�5]      	� -   5        $ tipo1��5]   :   $��	Tributado��6&   $��75��6I   $
��Left=�6T   $ TipoT��5^   &   $
 TrueY�68   $��25��6I   	� 5`      -   5        $ tipo3��5`   >   $��Sub Tributado��6&   $	��100��6M   $
��Left=�6Y   $ TipoT��5a   &   $��25��68   	� 5d      -   5        $ tipo2��5d   7   $��Isento��6&   $��75��6F   $
��Left=�6Q   $ TipoT��5e   &   $��25��68   	� 5g      -   5        $ tipo4��5g   >   $��Não Tributado��6&   $	��100��6M   $
��Left=�6Y   $ TipoT��5h   &   $��25��68   5j      �� 5m      $��
Alíquota:��6    $��4��61   $��2��6>   $��0 0 15 0q�6N   $
��Left=�5n       $��Center=�6;     5o      -   5        $ cmbAliquota��5o   G   $��4��6*   $��3��67   $
��Left=�6\   $	��170��5p   *   $ id��66   $
 nome��6M   $��Center=�6f   �� 5s      $��	Preço 1:��6    $��5��60   $��0��6=   $��Center=�6M   $��Center=�5t         5u      -   5        $
 	txtPreco1��5u   /   $��5��6B   $��1��6O   $
��Left=�5v   /   $ Numeric=�6J   $ f4��6]   $��25��6g   $��70��6s   �� 5x      $��	Preço 2:��6    $��5��60   $��1��6=   $��70 0 0 0q�6M   $��Center=�5y       $��Center=�6=     5z      -   5        $
 	txtPreco2��5z   /   $��5��6B   $��1��6O   $��2��6_   $��Right=�5{   /   $ Numeric=�6K   $ f4��6^   $��0 0 35 0q�6h   $��25��6z   $��70��6�   �� 5}      $��	Preço 3:��6    $��5��60   $��3��6=   $��25 0 0 0q�6M   $
��Left=�5~       $��Center=�6;     5      -   5        $
 	txtPreco3��5   /   $��5��6B   $��3��6O   $
��Left=�5�   /   $ Numeric=�6J   $ f4��6]   $��25��6g   $��70��6s   $��80 0 0 0q�6~   5�      5�        5�      -   5        $ RadTabAddDetalhes��5�   Z   $��Detalhes��6!   $��30��63   $
��Left=�6?   $	��Top=�5�   !   $	��120��69   ���#EEEEEE.��5�      � $��20q�6    5�      �� 5�      $��40 �6(   �� 5�      �� 5�      �� 5�      �� 5�      $��40 �6(   �� 5�      $��40 �6(   	 5�      �� 5�      �� 5�      $	��220 �6+   �� 5�      �� 5�      $��4* �6+   .+�5�      �� $��2��6!   $��5��65   .+�5�      /	  )Telerik.Windows.Controls.RadColorSelector	  -   5         	 Name$ Selector��5�   3   $
��Left=�6E    	  MainPaletteHeaderText$ Cores do tema��5�   3     	  StandardPaletteHeaderText$ Cores Padrão��6Y     	  StandardPaletteVisibility$ hidden=�6�    	  MainPalette$ Standard=�5�   3    	  NoColorText$ Sem Cor��6J    	  AutomaticColor$ Transparent��6`   5�   3   5�      �� 5�      $��Cor Selecionada:��6    $��1��68   $��2��6H   $��0 0 15 0q�6\   $��right=�5�       $��Center=�6<   �� 5�      -   5        $��	Retangulo��5�       $��25��61   $��25��6<   $��3��6H   $
��left=�6X   ��5�      �� 5�       �� Color$ Transparent��6.   5�      �� 5�      $��Ordem Cardápio:��6    $��1��67   $��2��6G   $��1��6[   $��0 0 15 0q�5�       $��right=�62   $��Center=�6N     5�      -   5        $
 txtOrdemCardapio��5�   r      	MaxLength$ 3��5�   /   $��3��6=   $��3��6M   $��1��6a   $ Numeric=�5�   /   $ d��6B   $��25��6K   $
��Left=�6W   $��Center=�6�   $��50��5�   /   �� 5�      $��Nome Cardápio:��6    $��1��66   $��2��6F   $��2��6Z   $��0 0 15 0q�6g   $��right=�5�       $��Center=�6<   �� 5�      -   5        $ txtNomeResumido��5�      $��3��5�      $��3��6.   $��2��6B   $��25��6O   $
��Left=�6[   $��Center=�5�   7   $	��150��6R   �� 5�      $��Código Mobile:��6    $��1��66   $��2��6F   $��3��6Z   $��0 0 15 0q�6g   $��right=�5�       $��Center=�6<   �� 5�      -   5        $ 	txtMobile��5�      $��3��5�      $��3��6.   $��3��6B   $��25��6O   $
��Left=�6[   $��Center=�5�   1   $	��150��6L   5�      5�        5�      -   5        $ RadTabAddImpressoras��5�   !   $��Plano(s) de fidelidade��5�   !   $��30��6A   $
��Left=�6M   $	��Top=�5�   ?   $	��130��6W   ���#EEEEEE.��5�      �  5�        Headerstyle �� 
TargetType9
  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property  �� Value! �� 
TargetType" ��Name��%�  &	       �� 5�      " 
 64   .�5�      ��  � ��$  25��67   �� 5�       � ��  5�   "   �� 5�   &   "! 
 66   .R�5�   *   �� $" borda1��61   ���#B68944���#49181A$��0 0 0 0q�5�   1   ._�5�   .   � 	 5�   2   �� 5�   6   $��70 �6G   �� 5�   6   $��* �6G   �� 5�   6   $	��100 �6G   .+�5�   2   �� ���#49181A$��1q�6O   $��0��6c   ._�5�   6   �� $��0 0 0 0q�6<   
�������$��Center=�5�   <   $��Center=�6Y   $��11(�6t   $��Código��6�   5�   3   �� 5�   2   ���#49181A$��1q�6O   $��1��6c   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   <   $��11(�6W   $
��Nome��6e   5�   3   �� 5�   2   ���#49181A$��1q�6O   $��2��6c   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   <   $��11(�6W   $��Pontos por real��6e   5�   3   5�   /   5�   +   5�   '   5�      5�      5�      .+�5�      *  $Telerik.Windows.Controls.RadGridView  -   5        #  Name$# gridFidelidade��5�   *   $	��280��6B   $	��280��6R   $	��580��6_   $��0��5�   *   $��2��6:   $ J� Opacity$	$ 0.9Q�6G   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl%   CanUserFreezeColumns% .  &   RowIndicatorVisibility$& 	Collapsed=�6r   '   AutoGenerateColumns' .  (   
IsReadOnly( .  $	��Top=�5�   Y   $��Center=�6q   )   ShowGroupPanel) .  *   HeaderRowStyle#* [ 5�   A   +   Columns+ 5�      1  +Telerik.Windows.Controls.GridViewDataColumn  5�      6  0Telerik.Windows.Controls.GridViewBoundColumnBase,   Header$, Código��69   -  'Telerik.Windows.Controls.GridViewColumn-   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$- 70 5�   9   .   IsGroupable. . /   IsFilterable/ . 0   DataMemberBinding0 5�   I   �� *IdFidelidade+5�   W     5�      $
, Nome��69   $- * 6j   . . / . 0 6G   �� *Nome+5�   L     5�      $, Pont os por real��69   $	- 100 5�   9   . . / . 0 5�   S   �� *Valor+5�   X   5�      5     5     5     �� 5	  
   $��Bottom=�6   $��1��6,   
�������$	��590��6Q   $��55��6]   $��0,0,-4,0q�5
     $��2��6#   ._�5     � .+�5     (  "Telerik.Windows.Controls.RadButton  -   5        1  Name$1 	btnSalvar��5  $   $��Salvar��67   $��0 0 100 -10q�6H   $��Right=�6]   $��80��5  $   $��25��6/     BotaoMenor2#��[ 6S     5     -   5        $1 btnCancelar��5  $   $��Cancelar��69   $��
0 0 10 -10q�6L   $��Right=�5  $   $��80��6@   $��25��6K   #��[ 6q   5     5     5     5     