   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa1    +Artebit.Restaurante.Caixa.Cadastro.Aliquota   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      .��5      � $	��630��6   $	��630��6     � 	Resources  5	   
      Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property �� Value �� 
TargetType ��Name � ColumnDefinitions��%�  &	        �� 5
      "  6(   .�5      ��  � ��$ 25��6+   �� 5       � �� 5      �� 5      "  6*   .R�5      �� $ borda1��6%   ���#B68944���#49181A$��0 0 0 0q�5   %   ._�5   "   �  5   &   �� 5   *   $��70 �6;   �� 5   *   $��70 �6;   �� 5   *   $��* �6;   �� 5   *   $	��100 �6;   �� 5   *   $��70 �6;   �� 5   *   $��70 �6;   �� 5   *   $��50 �6;   .+�5    &   �� ���#49181A$��1q�5"   -   $��0��5#   -   ._�5%   *   �� $��0 0 0 0q�60   
�������$��Center=�5'   4   $��11(�5(   4   $��Código��5)   4   5*   '   �� 5,   &   ���#49181A$��1q�5.   -   $��1��5/   -   ._�51   *   �� $��Center=�60   
�������$��Center=�53   4   $��11(�54   4   $��	Posição��55   4   56   '   �� 59   &   ���#49181A$��1q�5;   -   $��2��5<   -   ._�5>   *   �� $��Center=�60   
�������$��Center=�5@   4   $��11(�5A   4   $��	Alíquota��5B   4   5C   '   �� 5F   &   ���#49181A$��1q�5H   -   $��3��5I   -   ._�5K   *   �� $��Center=�60   
�������$��Center=�5M   4   $��11(�5N   4   $��Tipo de Imposto��5O   4   5P   '   �� 5S   &   ���#49181A$��1q�5U   -   $��4��5V   -   ._�5X   *   �� $��Center=�60   
�������$��Center=�5Z   4   $��11(�5[   4   $	��ALQ��5\   4   5]   '   �� 5_   &   ���#49181A$��1q�5a   -   $��5��5b   -   ._�5d   *   �� $��Center=�60   
�������$��Center=�5f   4   $��11(�5g   4   $	��CST��5h   4   5i   '   �� 5k   &   ���#49181A$��1q�5m   -   $��6��5n   -   ._�5p   *   �� $��Center=�60   
�������$��Center=�5r   4   $��11(�5s   4   $��Status��5t   4   5u   '   5w   #   5x      5{      5}      5~      5       � RowDefinitions 5�   
   �� 5�      $��35 �6   �� 5�      $	��43* �6   �� 5�      $
��492* �6    5�   
   �� 5�      $	��10* �6   .+�5�   
   �� -   5        $��	Adicionar��5�      $��0��6"   $��1��62   $	��100��6?   $��27��6K   $
��Left=�6W   $��5,10,0,0q�6r   $	��Top=�6�     BotaoMenor2#��[ 6�   ��5�      �� 5�      .R�5�      �� .+�5�      �� $��-40 -10 0 0q�6!   .+�5�      ��  �� Left$ 0��6%   
	 �� Top$	 0��65   $3��-pack://application:,,,/Img/adicionarItens.png��6D   $��20��6{   $��20��6�   $
��Fill=�6�   $
��Left=�6�   5�      �� 5�      $��	Adicionar��6$   $
��Bold"�65   $��12(�6G   $��
-10 -9 0 0q�6U   5�      5�      5�      �� 5�   
   -   5        $��Editar��5�      $��0��6   $��1��6/   $	��100��6<   $��27��6H   $
��Left=�6T   $	��Top=�6o   $��115,10,0,11q�6�   #��[ 6�   ��5�      �� 5�      .R�5�      �� .+�5�      �� $��-40 -10 0 0q�6!   .+�5�      �� $ 0��6%   $	 0��65   $+��%pack://application:,,,/Img/editar.png��6D   $��20��6s   $��20��6   $
��Fill=�6�   $
��Left=�6�   5�      �� 5�      $��Editar��6$   $
��Bold"�62   $��12(�6D   $��
-10 -9 0 0q�6R   5�      5�      5�      *  $Telerik.Windows.Controls.RadGridView  5�   
   -   5        
  Name$
 	gridDados��5�      $	��520��61   $	��520��6A   $	��980��6N   $��0��6Z   $��2��6j    J� Opacity$	 0.9Q�6w   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�5�   .      AutoGenerateColumns .     
IsReadOnly . $	��Top=�6      ShowGroupPanel .     HeaderRowStyle# [  5�   c      Columns 5�      1  +Telerik.Windows.Controls.GridViewDataColumn  5�      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Código��6-   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ 70 6f      IsGroupable .     IsFilterable .     DataMemberBinding 6=   �� *
idAliquota+6�     5�      $ Plano��6-   $ 70 6b    .  .   6<   �� *	posicao+6�     5�      $
 Tipo��6-   $ * 6b    .   .   6;   �� *
aliquota+6�     5�      $ Moeda��6-   $	 100 6d    .   .   6<   �� *	tpImposto+6�     5�      $ Moeda��6-   $ 70 6^    .   .   6<   �� *alq+6}     5�      $ Moeda��6-   $ 70 6^    .   .   6<   �� *cst+6}     5�      $ Status��6-   $ 50 6a    .   .   6=   �� *ativo+6�   5�      5�      5�      