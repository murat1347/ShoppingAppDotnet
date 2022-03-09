import { useState, useEffect } from 'react';

const initFormValue = {
  fullname: '',
  phone: '',
};

function Form({ addContact, contacts }) {
  const [form, setForm] = useState(initFormValue);

  useEffect(() => {
    setForm(initFormValue);
  }, [contacts]);

  const onChangeInput = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const onSubmit = (e) => {
    e.preventDefault();

    if (form.fullname === '' || form.phone === '') return false;

    addContact([...contacts, form]);
  };

  return (
    <form onSubmit={onSubmit}>
      <div>
        <input
          name='fullname'
          value={form.fullname}
          placeholder='Fullname'
          onChange={onChangeInput}
        ></input>
      </div>
      <div>
        <input
          name='phone'
          value={form.phone}
          placeholder='Phone Number'
          onChange={onChangeInput}
        ></input>
      </div>
      <div className='btn'>
        <button>Add</button>
      </div>
    </form>
  );
}

export default Form;
