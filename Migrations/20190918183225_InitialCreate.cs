using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProyecto.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caracteristicas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caracteristicas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha_Pedido = table.Column<DateTime>(nullable: false),
                    Fecha_Entrega = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Precio_Envio = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tiendas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Contraseña = table.Column<string>(nullable: true),
                    Telefono = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiendas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Contraseña = table.Column<string>(nullable: true),
                    Telefono = table.Column<int>(nullable: false),
                    Direcction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "opciones",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<int>(nullable: false),
                    CaracteristicaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_opciones_caracteristicas_CaracteristicaID",
                        column: x => x.CaracteristicaID,
                        principalTable: "caracteristicas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Peso = table.Column<float>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Tienda_ID = table.Column<int>(nullable: false),
                    Caracteristica_ID = table.Column<int>(nullable: false),
                    TiendaID = table.Column<int>(nullable: true),
                    CaracteristicaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_productos_caracteristicas_CaracteristicaID",
                        column: x => x.CaracteristicaID,
                        principalTable: "caracteristicas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productos_tiendas_TiendaID",
                        column: x => x.TiendaID,
                        principalTable: "tiendas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "carritos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(nullable: false),
                    SubTotal = table.Column<int>(nullable: false),
                    ProductoID = table.Column<int>(nullable: false),
                    UsuarioID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carritos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_carritos_productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "productos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carritos_usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "producto_categorias",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductoID = table.Column<int>(nullable: false),
                    CategoriaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto_categorias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_producto_categorias_categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "categorias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_producto_categorias_productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "productos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detalle_pedidos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarritoProductoID = table.Column<int>(nullable: false),
                    CarritoUsuarioID = table.Column<int>(nullable: false),
                    CarritoID = table.Column<int>(nullable: false),
                    PedidoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_pedidos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_detalle_pedidos_carritos_CarritoID",
                        column: x => x.CarritoID,
                        principalTable: "carritos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_pedidos_pedidos_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "pedidos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carritos_ProductoID",
                table: "carritos",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_carritos_UsuarioID",
                table: "carritos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedidos_CarritoID",
                table: "detalle_pedidos",
                column: "CarritoID");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_pedidos_PedidoID",
                table: "detalle_pedidos",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_opciones_CaracteristicaID",
                table: "opciones",
                column: "CaracteristicaID");

            migrationBuilder.CreateIndex(
                name: "IX_producto_categorias_CategoriaID",
                table: "producto_categorias",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_producto_categorias_ProductoID",
                table: "producto_categorias",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_productos_CaracteristicaID",
                table: "productos",
                column: "CaracteristicaID");

            migrationBuilder.CreateIndex(
                name: "IX_productos_TiendaID",
                table: "productos",
                column: "TiendaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalle_pedidos");

            migrationBuilder.DropTable(
                name: "opciones");

            migrationBuilder.DropTable(
                name: "producto_categorias");

            migrationBuilder.DropTable(
                name: "carritos");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "caracteristicas");

            migrationBuilder.DropTable(
                name: "tiendas");
        }
    }
}
