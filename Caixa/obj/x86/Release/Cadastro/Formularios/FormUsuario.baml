   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa@    :Artebit.Restaurante.Caixa.Cadastro.Formularios.FormUsuario   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   $	��400��$	��600��) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�   CanMove . $��Usuarios��( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�
������ .���  � RowDefinitions �� �� $��50 �����  �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��.+�, 	 &Telerik.Windows.Controls.RadTabControl  -   $��radTabControl1��$��Stretch=�$��0q�$��Stretch=�; 	 5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=�.@�) 	 #Telerik.Windows.Controls.RadTabItem  -   $��RadTabEditar��$��Informações��$��30��$
��Left=�$	��Top=�$	��120�����#EEEEEE.��� $��0 15 0 0q� �� �� �� �� �� �� $��3* � � ColumnDefinitions �� �� $��2* �.+��� $��	Usuário:��$��0��$��Center=�$��Center=�$
��Bold"��� $��Nome:��$��1��$��Center=�$��Center=��� $��Senha:��$��2��$��Center=�$��Center=��� $��Ativo:��$��3��$��Center=�$��Center=�/  )Telerik.Windows.Controls.RadMaskedTextBox  -   $��txtboxCodUsuario��$��1��$��0��$��25��   MaskType$
 None=�   EmptyContent$ Digite o código��$	��270��$
��Left=�  -   $��
txtboxNome��$��1��$��1��$��25��$
 None=�$ Digite o nome��$	��270��$
��Left=�G� -   $��senha��$��1��$��2��$��25��$	��270��$
��Left=��� -   $��checkUsuario��$��1��$��3��$��Center=�  -   $��RadTabAddMesa��$��Perfil��$��30��$
��Left=�$	��Top=�$��5q�$	��120�����#EEEEEE.��� -   $��	GridPefil��$��3q� �� $��35 ���  �� $	��171 ��� $
��319* ��� $��90 �.+��� $��Sistema:��$
��Bold"�$��Center=�$��Center=�$��	2,10,0,10q��� $��Perfil:��$
��Bold"�$��1��$
��Left=�$��Center=�$��	5,10,0,10q��� $��Supervisor:��$
��Bold"�$��2��$
��Left=�$��Center=��� $��1��$��3��.��� -	   $��
gridScroll�� �� �� $��2* ��� $��40 ��� $��Bottom=�$��1��
�������$	��590��$��55��._�� .+�(  "Telerik.Windows.Controls.RadButton  -
   $��	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[  