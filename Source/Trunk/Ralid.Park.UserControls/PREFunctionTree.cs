﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;
using System.Reflection;

namespace Ralid.Park.UserControls
{
    public partial class PREFunctionTree : TreeView
    {
        private readonly string RootText = Resources.Resource1.FunctionTree_Root;

        private List<TreeNode> _rightNodes = new List<TreeNode>();

        public PREFunctionTree()
        {
            InitializeComponent();
        }

        public PREFunctionTree(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region 公共方法和属性
        public void Init()
        {
            this.ImageList = images;
            this.CheckBoxes = true;
            this.Nodes.Clear();
            TreeNode root = new TreeNode(RootText,0,0);
            this.Nodes.Add(root);
            List<TreeNode> catalogs = new List<TreeNode>();
            Type operatorEnum = typeof(PREPermission);
            FieldInfo[] fields = operatorEnum.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    object[] attrs = field.GetCustomAttributes(false);
                    foreach (object attr in attrs)
                    {
                        if (attr is OperatorRightAttribute)
                        {
                            OperatorRightAttribute right = attr as OperatorRightAttribute;
                            PREPermission permission = (PREPermission)field.GetValue(null);
                            //if (GlobalVariables.IsNETParkAndOffLie)
                            //{
                            //    //写卡模式，不显示的权限
                            //    if (permission == Permission.RemoteReadCard)
                            //    {
                            //        continue;
                            //    }
                            //}
                            //else
                            //{ 
                            //    //其他模式，不显示的权限
                            //    if (permission == Permission.DownloadAllCards)
                            //    {
                            //        continue;
                            //    }
                            //}

                            string catalog = Ralid.Park.BusinessModel.Resouce.PermissionDescription.GetPREDescription(right.Catalog);
                            TreeNode catalogNode = catalogs.Find(t => t.Text == catalog);
                            if (catalogNode == null)
                            {
                                catalogNode = new TreeNode(catalog,0,0);
                                root.Nodes.Add(catalogNode);
                                catalogs.Add(catalogNode);
                            }
                            string temp = Ralid.Park.BusinessModel.Resouce.PermissionDescription.GetDescription(permission);
                            TreeNode rightNode = new TreeNode(temp,1,1);
                            rightNode.Tag = permission;
                            catalogNode.Nodes.Add(rightNode);
                            _rightNodes.Add(rightNode);
                        }
                    }
                }
            }
            root.Expand();
            this.AfterCheck += Node_Checked;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedRights
        {
            get
            {
                if (this.Nodes[0].Checked)
                {
                    return "all";
                }
                else
                {
                    List<PREPermission> rights = new List<PREPermission>();
                    foreach (TreeNode node in _rightNodes)
                    {
                        if (node.Checked)
                        {
                            rights.Add((PREPermission)node.Tag);
                        }
                    }
                    var list = from right in rights 
                               select right.ToString("D");
                    return string.Join(",", list.ToArray());
                }
                
            }
            set
            {
                if (value.ToLower() == "all")
                {
                    this.Nodes[0].Checked = true;
                }
                else if (!string.IsNullOrEmpty(value))
                {
                    foreach (string str in value.Split(','))
                    {
                        int i;
                        if (int.TryParse(str, out i))
                        {
                            TreeNode node = _rightNodes.Find(n => (int)n.Tag == i);
                            if (node != null)
                            {
                                node.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 私有方法
        private void Node_Checked(object sender, TreeViewEventArgs e)
        {
            this.AfterCheck -= Node_Checked;
            CheckChildren(e.Node);
            CheckParent(e.Node);
            this.AfterCheck += Node_Checked;
        }
        private void CheckChildren(TreeNode curNode)
        {
            foreach (TreeNode nod in curNode.Nodes)
            {
                nod.Checked = curNode.Checked;
                CheckChildren(nod);
            }
        }
        private void CheckParent(TreeNode curNode)
        {
            TreeNode parent = curNode.Parent;
            if (parent != null)
            {
                bool allChecked = true;
                foreach (TreeNode n in parent.Nodes)
                {
                    if (n.Checked == false)
                    {
                        allChecked = false;
                        break;
                    }
                }
                parent.Checked = allChecked;
                CheckParent(parent);
            }
        }
        #endregion
    }
}
