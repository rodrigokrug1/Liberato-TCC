CREATE FUNCTION [dbo].[FC_VALIDA_CNPJCPF]
(
	@p_cpfCnpj		CHAR(14)
)
RETURNS BIT
AS
BEGIN
	DECLARE 
		@v_cpfCnpj			  VARCHAR(14),
        @v_indice			  INT,
        @v_soma				  INT,
		@v_digito_1		      INT,
        @v_digito_2			  INT,
        @v_var_1			  INT,
        @v_var_2			  INT,
		@v_contador_1		  INT,
        @v_contador_2		  INT,
		@r_valido			  BIT	

			SET @v_cpfCnpj = LTRIM(RTRIM(ISNULL(@p_cpfCnpj,'')))
	
	--CPF
	IF (LEN(@v_cpfCnpj) = 11)
		BEGIN
			SET @v_cpfCnpj = SUBSTRING(@v_cpfCnpj, 1, 9) 
			SET @v_contador_1 = 2
			SET @v_digito_1 = 0
 
			WHILE (@v_contador_1 < = 10)
			BEGIN 
				SET @v_digito_1 = @v_digito_1 + (@v_contador_1 * CAST(SUBSTRING(@v_cpfCnpj, 11 - @v_contador_1, 1) AS INT))
				SET @v_contador_1 = @v_contador_1 + 1
			END 
 
			SET @v_digito_1 = @v_digito_1 - (@v_digito_1/11)*11
 
			IF (@v_digito_1 <= 1)
				SET @v_digito_1 = 0
			ELSE 
				SET @v_digito_1 = 11 - @v_digito_1
 
			SET @v_cpfCnpj = @v_cpfCnpj + CAST(@v_digito_1 AS VARCHAR(1))
 
			IF (@v_cpfCnpj <> SUBSTRING(@p_cpfCnpj, 1, 10))
				SET @r_valido = 0 
			ELSE 
			BEGIN
				SET @v_digito_2 = 0
				SET @v_contador_2 = 2
 
				WHILE (@v_contador_2 < = 11)
				BEGIN 
					SET @v_digito_2 = @v_digito_2 + (@v_contador_2 * CAST(SUBSTRING(@v_cpfCnpj, 12 - @v_contador_2, 1) AS INT))
					SET @v_contador_2 = @v_contador_2 + 1
				END 
 
				SET @v_digito_2 = @v_digito_2 - (@v_digito_2/11)*11
 
				IF (@v_digito_2 < 2)
					SET @v_digito_2 = 0
				ELSE 
					SET @v_digito_2 = 11 - @v_digito_2
 
				SET @v_cpfCnpj = @v_cpfCnpj + CAST(@v_digito_2 AS VARCHAR(1))
 
				IF (@v_cpfCnpj <> CAST(@p_cpfCnpj AS VARCHAR))
					SET @r_valido = 0	
				ELSE	
					SET @r_valido = 1		                
			END	
		RETURN @r_valido
	END			

	--CNPJ
	ELSE 
		BEGIN
			SET @v_cpfCnpj = LTRIM(RTRIM(@p_cpfCnpj))
			SET @v_soma = 0
			SET @v_indice = 1
			SET @r_valido = 0
			SET @v_var_1 = 5

			WHILE ( @v_indice < = 4 )
				BEGIN
					SET @v_soma = @v_soma + CONVERT(INT, SUBSTRING(@v_cpfCnpj, @v_indice, 1)) * @v_var_1
					SET @v_indice = @v_indice + 1 
					SET @v_var_1 = @v_var_1 - 1 
				END 
       
			SET @v_var_2 = 9

			WHILE ( @v_indice <= 12 )
				BEGIN
					SET @v_soma = @v_soma + CONVERT(INT, SUBSTRING(@v_cpfCnpj, @v_indice, 1)) * @v_var_2
					SET @v_indice = @v_indice + 1
					SET @v_var_2 = @v_var_2 - 1            
				END
 
			SET @v_digito_1 = ( @v_soma % 11 ) 

			IF @v_digito_1 < 2
				SET @v_digito_1 = 0;
			ELSE
				SET @v_digito_1 = 11 - ( @v_soma % 11 );  
				SET @v_indice = 1
				SET @v_soma = 0
				SET @v_var_1 = 6
				SET @r_valido = 0
			  
			WHILE ( @v_indice <= 5 )
				BEGIN
					SET @v_soma = @v_soma + CONVERT(INT, SUBSTRING(@v_cpfCnpj, @v_indice, 1)) * @v_var_1
					SET @v_indice = @v_indice + 1
					SET @v_var_1 = @v_var_1 - 1
				END 

			SET @v_var_2 = 9

			WHILE ( @v_indice <= 13 )
				BEGIN
					SET @v_soma = @v_soma + CONVERT(INT, SUBSTRING(@v_cpfCnpj, @v_indice, 1)) * @v_var_2
					SET @v_indice = @v_indice + 1
					SET @v_var_2 = @v_var_2 - 1            
				END 
				SET @v_digito_2 = ( @v_soma % 11 )   
			IF @v_digito_2 < 2
				SET @v_digito_2 = 0 
			ELSE
				SET @v_digito_2 = 11 - ( @v_soma % 11 ) 
			IF ( @v_digito_1 = SUBSTRING(@v_cpfCnpj, LEN(@v_cpfCnpj) - 1, 1) ) AND ( @v_digito_2 = SUBSTRING(@v_cpfCnpj, LEN(@v_cpfCnpj), 1) )
				SET @r_valido = 1
			ELSE
				SET @r_valido = 0  			
		END 
	RETURN @r_valido
END