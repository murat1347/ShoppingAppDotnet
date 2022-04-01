import {BaseModel} from "./basemodel";

export class Category implements BaseModel{

  public static NameMaxLength = 32;
  public static NameMinLength = 2;

  constructor(
    public id?: number,
    public parentId?: number,
    public name?: string) {}
}
