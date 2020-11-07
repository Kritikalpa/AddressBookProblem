CREATE PROCEDURE [dbo].[spRetrieveContact]
	@choice INT
AS
IF @choice = 1
BEGIN
	SELECT id, first_name, last_name, address, city, state,email, zip, phone_number FROM addressBook
END

