import { Component, OnInit } from '@angular/core';
import {Category} from "../../../model/category";
import {CategoryService} from "./category.service";
import {FlatTreeControl} from "@angular/cdk/tree";
import {MatTreeFlatDataSource, MatTreeFlattener} from "@angular/material/tree";
import {SelectionModel} from "@angular/cdk/collections";
import {RouteValues} from "../../../app-routing.module";

class CategoryItemNode {
  id!:number;
  children!: CategoryItemNode[];
  item!: string;
}

class CategoryItemFlatNode {
  id!: number;
  item!: string;
  level!: number;
  expandable!: boolean;
}

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  RouteValues = RouteValues;

  flatNodeMap = new Map<CategoryItemFlatNode, CategoryItemNode>();
  nestedNodeMap = new Map<CategoryItemNode, CategoryItemFlatNode>();

  treeControl: FlatTreeControl<CategoryItemFlatNode>;
  treeFlattener: MatTreeFlattener<CategoryItemNode, CategoryItemFlatNode>;
  dataSource: MatTreeFlatDataSource<CategoryItemNode, CategoryItemFlatNode>;

  public categories : Category[] = [];

  constructor(private _categoryService: CategoryService) {
    this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
      this.isExpandable, this.getChildren);
    this.treeControl = new FlatTreeControl<CategoryItemFlatNode>(this.getLevel, this.isExpandable);
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
    this.dataSource.data = [];
  }

  getLevel = (node: CategoryItemFlatNode) => node.level;

  isExpandable = (node: CategoryItemFlatNode) => node.expandable;

  getChildren = (node: CategoryItemNode): CategoryItemNode[] => node.children;

  hasChild = (_: number, _nodeData: CategoryItemFlatNode) => _nodeData.expandable;

  transformer = (node: CategoryItemNode, level: number) => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode = existingNode && existingNode.item === node.item
      ? existingNode
      : new CategoryItemFlatNode();
    flatNode.item = node.item;
    flatNode.id = node.id;
    flatNode.level = level;
    flatNode.expandable = !!node.children?.length;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  }

  /** Select the category so we can insert the new item. */
  delete(node: CategoryItemFlatNode) {
    const parentNode = this.flatNodeMap.get(node);
    this._categoryService.delete(node.id).subscribe(_=>{
        this.categories= this.categories.filter(cat=>cat.id != node.id);
        this.dataSource.data = this.convertCategories(this.categories);
    })
  }

  /** Save the node to database */
  saveNode(node: CategoryItemFlatNode, itemValue: string) {
    const nestedNode = this.flatNodeMap.get(node);
    //this._database.updateItem(nestedNode!, itemValue);
  }

  ngOnInit(): void {
    this.getCategories();
  }

  static categoryToItemNode(category: Category){
    let node = new CategoryItemNode();
    node.item = category.name!;
    node.id = category.id!;
    return node;
  }

  findChildren(categoryItemNode : CategoryItemNode){
    this.categories.filter(c=> c.parentId == categoryItemNode.id).forEach(c=>{
      let newCategoryItemNode = CategoryComponent.categoryToItemNode(c);
      if(!categoryItemNode.children){
        categoryItemNode.children = [];
      }
      categoryItemNode.children.push(newCategoryItemNode);
      this.findChildren(newCategoryItemNode);
    });
  }

  convertCategories(categories: Category[]) : CategoryItemNode[]{

    let categoryItemNodes : CategoryItemNode[] = categories
      .filter(c=>categories.every(c2=>c2.id != c.parentId))
      .map(c=>{
        let cat = new CategoryItemNode();
        cat.item = c.name!;
        cat.id = c.id!;
        return cat;
      });

    categoryItemNodes.forEach(c=>{
      this.findChildren(c);
    })

    return categoryItemNodes;
  }

  getCategories():void {
    this._categoryService.getAll().subscribe(categories =>{

      this.categories=categories;

      this.dataSource.data = this.convertCategories(categories);
    });
  }
}
