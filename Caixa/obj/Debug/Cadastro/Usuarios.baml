   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa1    +Artebit.Restaurante.Caixa.Cadastro.Usuarios   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      .��5      � $	��630��6   $	��630��6     � 	Resources  5	   
      Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property �� Value �� 
TargetType ��Name � ColumnDefinitions��%O  &	        �� 5
      "  6(   .�5      ��  � ��$ 25��6+   �� 5       � �� 5      �� 5      "  6*   .R�5      �� $ borda1��6%   ���#B68944���#49181A$��0 0 0 0q�5   %   ._�5   "   �  5   &   �� 5   *   $	��100 �6;   �� 5   *   $��* �6;   �� 5   *   $	��120 �6;   �� 5   *   $	��120 �6;   �� 5   *   $��50 �6;   .+�5   &   �� ���#49181A$��1q�5    -   $��0��5!   -   ._�5#   *   �� $��0 0 0 0q�60   
�������$��Center=�5%   4   $��11(�5&   4   $��Código��5'   4   5(   '   �� 5*   &   ���#49181A$��1q�5,   -   $��1��5-   -   ._�5/   *   �� $��Center=�60   
�������$��Center=�51   4   $��11(�52   4   $
��Nome��53   4   54   '   �� 57   &   ���#49181A$��1q�59   -   $��2��5:   -   ._�5<   *   �� $��Center=�60   
�������$��Center=�5>   4   $��11(�5?   4   $��Data Criação��5@   4   5A   '   �� 5D   &   ���#49181A$��1q�5F   -   $��3��5G   -   ._�5I   *   �� $��Center=�60   
�������$��Center=�5K   4   $��11(�5L   4   $��Ultima Alteração��5M   4   5N   '   �� 5R   &   ���#49181A$��1q�5T   -   $��4��5U   -   ._�5W   *   �� $��Center=�60   
�������$��Center=�5Y   4   $��11(�5Z   4   $��Ativo��5[   4   5\   '   5]   #   5^      5_      5a      5b      5c       � RowDefinitions 5d   
   �� 5e      $��35 �6   �� 5f      $	��43* �6   �� 5g      $
��492* �6    5i   
   �� 5j      $	��10* �6   .+�5m   
   �� -   5        $��	Adicionar��5m      $��0��6"   $��1��62   $	��100��6?   $��27��6K   $
��Left=�6W   $��5,10,0,0q�6r   $	��Top=�6�     BotaoMenor2#��[ 6�   ��5n      �� 5o      .R�5p      �� .+�5q      �� $��-40 -10 0 0q�6!   .+�5r      ��  �� Left$ 0��6%   
	 �� Top$	 0��65   $3��-pack://application:,,,/Img/adicionarItens.png��6D   $��20��6{   $��20��6�   $
��Fill=�6�   $
��Left=�6�   5s      �� 5t      $��	Adicionar��6$   $
��Bold"�65   $��12(�6G   $��
-10 -9 0 0q�6U   5v      5w      5y      �� 5z   
   -   5        $��Editar��5z      $��0��6   $��1��6/   $	��100��6<   $��27��6H   $
��Left=�6T   $	��Top=�6o   $��115,10,0,11q�6�   #��[ 6�   ��5{      �� 5|      .R�5}      �� .+�5~      �� $��-40 -10 0 0q�6!   .+�5      �� $ 0��6%   $	 0��65   $+��%pack://application:,,,/Img/editar.png��6D   $��20��6s   $��20��6   $
��Fill=�6�   $
��Left=�6�   5�      �� 5�      $��Editar��6$   $
��Bold"�62   $��12(�6D   $��
-10 -9 0 0q�6R   5�      5�      5�      *  $Telerik.Windows.Controls.RadGridView  5�   
   -   5        
  Name$
 	gridDados��5�      $	��520��6x   $	��520��6�   $	��980��6�   $��0��6�   $��2��6�    J� Opacity$	 0.9Q�6�   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�5�   .      AutoGenerateColumns .     
IsReadOnly . $	��Top=�6      ShowGroupPanel .     HeaderRowStyle# [  5�   =   * 	 $Telerik.Windows.Controls.DataControl   ItemsSource 5�   W   �� *
GUSUARIO+   Columns 5�      1  +Telerik.Windows.Controls.GridViewDataColumn  5�      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Código��6-   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100 6f      IsGroupable .    IsFilterable .    DataMemberBinding 6=   �� *
codUsuario+6�     5�      $
 Nome��6-   $ * 6^    .  .  6;   �� *nome+6{     5�      $ Data Inculusão��6-   $	 120 6p    .  .  6E   �� *dataInclusao+6�     5�      $ Ultima Alteração��6-   $	 120 6s    .  .  6G   �� *dataAlteracao+6�     5�      $ Ativo��6-   $ 50 6`    .   .   6<   �� *ativo+6   5�      5�      5�      