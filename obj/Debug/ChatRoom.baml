   M S B A M L   `   `   `  ����J  FChatRoomProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ChatRoomProjectChatRoomProject  5   	       ChatRoomProject.ChatRoom   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   )localclr-namespace:ChatRoomProject  5   	     � Title$  ChatRoom��5	   	   $	��450��6   $	��800��6'    � WindowStyle$
 None=�64    � 
ResizeMode$ NoResize=�6G    � Topmost . .��5
      � ��5   
   �� 5       �� 
StartPoint$ 0.1,0&�6"    �� EndPoint$ 0.9,1&�65   .��5      �  � Color$ 	#FFE63070��6    � Offset$ 1Q�61   � 5      $ 	#FFFE8704��6   $ 0Q�61   5      .+�5   
   �� -   5        $	��390��5      $	��Top=�6    �� CornerRadius$ 	0 0 180 0��66   ���#100E176�   �� 5   
   $	��100��6   $��
686,0,14,0q�6!   .+�5      �� -   5        	 ��Name$	 btnExit��5   "   $��22,20q�5       &  "MaterialDesignFloatingActionButton#��[  6$   
 � ToolTip$
 Close��5      ��5   `   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6*   
�������$��30��6J   $��30��6U   5      5      �� 5   
   -   5        $	 Connectbutton��5      $��	Connexion��6)   $��434,80,259,338q�6Z   $��32��6r   �� 5    
   $��19,80,50,32q�6   .+�5!      �� -   5         ��Name$ IpAdressTexttextBox��5!      ���	#FFF7EDED
���   �$��31��6`   $��
0,0,554,15q�6l   $��Adresse Ip ChatRoom :��6�    �� 
IsReadOnly . 6�   �� 5"      -   5        $ NomChatRoom��5"      ���	#FFF7EDED
���   �$��31��6X   $��200,-47,350,15q�6d   $�� ��6}   �� 5$      -   5        $ ChatScreentextBox��5$      ���	#FFF7EDED
���   �$	��200��6\    . 6i   �� 5%      -   5        $ MessagetextBox��5%      ���	#FFF7EDED
���   �$��40��6[   $��	0,15,73,0q�6g   $�� ��6{   �� 5&      -   5        $	 
Sendbutton��5&      $��Envoi��6B   $��40��6R   $��664,-40,0,0q�6^   5'      5(      5)      