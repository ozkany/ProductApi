import React, { useState, useEffect } from "react";
import { makeStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import { useParams } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: '100%',
    marginTop: theme.spacing(3),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

export default function ProductUpdate() {
  const classes = useStyles();

  const { id } = useParams();
  useEffect(() => {
    fetch("http://localhost:8080/api/Products/"+id)
      .then(res => res.json())
      .then(
        (result) => {
          setId(result.id)
          setName(result.name)
          setBrand(result.brand)
          setDescription(result.description)
          setPrice(result.price)
          setStock(result.stock)
        }
      )
  }, [id])

  const handleSubmit = event => {
    event.preventDefault();
    var data = {
      'id': productId,
      'name': name,
      'brand': brand,
      'description': description,
      'price': price,
      'stock': stock,
    }
    fetch('http://localhost:8080/api/Products', {
      method: 'PUT',
      headers: {
        Accept: 'application/form-data',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    })
    .then(
      (result) => {
        if (result.status == '200') {
          alert('Product is successfully updated.');
          window.location.href = '/';
        }
      }
    )
  }

  const [productId, setId] = useState('');
  const [name, setName] = useState('');
  const [brand, setBrand] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [stock, setStock] = useState('');

  return (
    <Container maxWidth="xs">
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Product
        </Typography>
        <form className={classes.form} onSubmit={handleSubmit}>
        <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="id"
                label="Id"
                value={productId}
                onChange={(e) => setId(e.target.value)}
                autoFocus
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="name"
                label="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="brand"
                label="Brand"
                value={brand}
                onChange={(e) => setBrand(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="description"
                label="Description"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="price"
                label="Price"
                value={price}
                onChange={(e) => setPrice(e.target.value)}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="stock"
                label="Stock"
                value={stock}
                onChange={(e) => setStock(e.target.value)}
              />
            </Grid>
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Update
          </Button>
        </form>
      </div>
    </Container>
  );
}
