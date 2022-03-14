import React from "react";
import TextInput from "../toolbox/TextInput";
import SelectInput from "../toolbox/SelectInput";

const ProductDetail = ({ categories, product, onSave, onChange,errors }) => {
  return (
    <form onSubmit={onSave}>
      <h2>{product.id ? "Güncelle" : "Ekle"}</h2>
      <TextInput
        name="name"
        label="Product Name"
        value={product.name}
        onChange={onChange}
        error={errors.name}
      />

      <SelectInput
        name="categoryId"
        label="Category"
        value={product.Id || ""}
        defaultOption="Seçiniz"
        options={categories.map(category => ({
          value: category.id,
          text: category.name
        }))}
        onChange={onChange}
        error={errors.categoryId}
      />

      <TextInput
        name="price"
        label="Unit Price"
        value={product.price}
        onChange={onChange}
        error={errors.price}
      />

      <TextInput
        name="description"
        label="Quantity Per Unit"
        value={product.description}
        onChange={onChange}
        error={errors.description}
      />

      <TextInput
        name="stock"
        label="Units In Stock"
        value={product.stock}
        onChange={onChange}
        error={errors.stock}
      />

      <button type="submit" className="btn btn-success">
        Kaydet
      </button>
    </form>
  );
};

export default ProductDetail;
